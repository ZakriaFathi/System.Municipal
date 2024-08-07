using FluentResults;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Contracts;
using Municipal.Application.Legacy.Features.Email;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.ForgotPassword;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.RefreshToken;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.ResetPassword;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingIn;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingInOtp;
using Municipal.Application.Legacy.Features.Identity.Auth.Commands.SingUp;
using Municipal.Application.Legacy.Features.Identity.Auth.Responses;
using Municipal.Domin.Models;
using Municipal.Utils.Enums;
using Municipal.Utils.Options;

namespace Municipal.DataAccess.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMailRepository _emailSender;
    private readonly ClientSettings _clientSettings;

    public AuthRepository(UserManager<AppUser> userManager, IMailRepository mailService, SignInManager<AppUser> signInManager, IOptions<ClientSettings> clientSettings)
    {
        _userManager = userManager;
        _emailSender = mailService;
        _signInManager = signInManager;
        _clientSettings = clientSettings.Value;
    }

    public async Task<Result<string>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        if (request.NewPassword != request.ConfiramNewPassword)
            return Result.Fail(new List<string>() { "كلمة المرور غير متطابقة" });

        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (result.Succeeded)
            return "تم تغيير كلمة المرور بنجاح";

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }
    public async Task<Result<string>> ForgotPassword(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callback =
            $"https://localhost:7017/api/Auth/ResetPassword?culture=ar-LY&userName={user.UserName}&token={token}";
        var message = new SendEmailRequest
        {
            Subject = "إعادة تعين كلمة المرور",
            ToEmail = user.Email,
            html = callback
        };
        await _emailSender.SendEmailAsync(message);
        return "تم الارسال";

    }
    public async Task<Result<AccessTokenRsponse>> SingIn(SingInRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        var password = await _userManager.CheckPasswordAsync(user, request.Password);
        if (password == false)
            return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

        if (user.ActivateState == ActivateState.InActive)
            return Result.Fail(new List<string>() { "هذا المستخدم غير مفعل" });

        using (var client = new HttpClient())
        {
            var disco = await client.GetDiscoveryDocumentAsync(_clientSettings.Url);
            //var disco = "http://172.20.10.3:8888/connect/token";
            var tokenResponse = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = IdConstants.GrantType.UserCredentials,
                ClientId = _clientSettings.ClientId[1],
                ClientSecret = _clientSettings.ClientSecrets,
                Parameters =
                    {
                        {"userName", request.UserName },
                        {"password", request.Password },
                        {"Scope", _clientSettings.Scopes[1]},
                    }
            });
            var response = new AccessTokenRsponse()
            {
                Access_token = tokenResponse.AccessToken,
                Expires_in = tokenResponse.ExpiresIn,
                Token_type = tokenResponse.TokenType,
                Scope = tokenResponse.Scope,
            };
            if (response.Access_token == null)
                return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");

            //if (user.TwoFactorEnabled)
            //{
            //    await _signInManager.SignOutAsync();
            //    await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            //    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            //    var message = new SendEmailRequest
            //    {
            //        Subject = "رمز المصادقة",
            //        ToEmail = user.Email,
            //        html = $" {token} This code is for two-factor authentication "
            //    };
            //    await _emailSender.SendEmailAsync(message);
            //}
            return response;
        }
    }
    public async Task<Result<AccessTokenRsponse>> SingInOtp(SingInOtpRequest request, CancellationToken cancellationToken)
    {
        var twoFactor = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        var result = await _signInManager.TwoFactorSignInAsync("Email", request.Otp, false, false);
        if (result == SignInResult.Failed)
            return Result.Fail(new List<string>() { "رمز المصادقة غير صحيح" });


        using (var client = new HttpClient())
        {
            var disco = await client.GetDiscoveryDocumentAsync(_clientSettings.Url);
            var tokenResponse = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = IdConstants.GrantType.Otp,
                ClientId = _clientSettings.ClientId[1],
                ClientSecret = _clientSettings.ClientSecrets,
                Parameters =
                    {
                        {"Otp", request.Otp },
                        {"UserName", twoFactor.UserName },
                        {"Scope", $"{_clientSettings.Scopes[0]} {_clientSettings.Scopes[2]}"},
                    }
            });
            var response = new AccessTokenRsponse()
            {
                Access_token = tokenResponse.AccessToken,
                Expires_in = tokenResponse.ExpiresIn,
                Token_type = tokenResponse.TokenType,
                Refresh_token = tokenResponse.RefreshToken,
                Scope = tokenResponse.Scope,
            };
            if (response.Access_token == null)
                return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");

            return response;
        }
    }
    public async Task<Result<RefreshTokenRsponse>> RefreshToken(RefreshTokenClientRequest request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var disco = await client.GetDiscoveryDocumentAsync(_clientSettings.Url);

                var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = _clientSettings.ClientId[1],
                    ClientSecret = _clientSettings.ClientSecrets,
                    RefreshToken = request.RefreshToken
                });

                var response = new RefreshTokenRsponse()
                {
                    Access_token = tokenResponse.AccessToken,
                };
                if (response.Access_token == null)
                    return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" );

                    return response;
            } 
        }
    public async Task<Result<string>> SingUp(SingUpRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(request.UserName) is not null)
            return Result.Fail(new List<string>() { "اسم المستخدم موجود مسبقا" });


        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            FatherName = request.FatherName,
            Nationality = request.Nationality,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            NationalId = request.NationalId,
            PassportId = request.PassportId,
            PassportExpirationDate = request.PassportExpirationDate,
            placeOfIssue = request.placeOfIssue,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address,
            UserName = request.UserName,
            UserType = UserType.Maker,
            ActivateState = ActivateState.InActive,
            TwoFactorEnabled = true,
            EmailConfirmed = true
        };
        await _userManager.CreateAsync(user, request.Password);

        if (request.Password.Length <= 7)
            return Result.Fail(new List<string>{ "كلمة المرور اقل من 8 " });

        //var message = new SendEmailRequest
        //{
        //    Subject = "إنشاء حساب",
        //    ToEmail = user.Email,
        //    html = $" تم إنشاء حساب بالفعل يمكنك استعمال هذا البريد بما يسمح لك باجراء العمليات المخصصه "
        //};
        //await _emailSender.SendEmailAsync(message);
        return user.Id;
    }
}