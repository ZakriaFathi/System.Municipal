using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangePassword;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangeUserActivation;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUser;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.CreateUserClimas;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUser;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserClaims;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserKyc;
using Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.UpdateUserProfile;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Models;

namespace Municipal.DataAccess.Repositories;

public class IdentityManagementRepository: IIdentityManagementService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IdentityUsersDbContext _dbContext;
    private readonly IMailRepository _emailSender;


    public IdentityManagementRepository( IdentityUsersDbContext dbContext, UserManager<AppUser> userManager, IMailRepository emailSender)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<Result<string>> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        var password = await _userManager.CheckPasswordAsync(user, request.OldPassword);
        if (password == false)
            return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (result.Succeeded)
            return "تم تغيير كلمة المرور بنجاح";

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> ChangeUserActivation(ChangeUserActivationRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.ActivateState = request.State;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ?
            "تم تغيير حالة المستخدم بنجاح" :
            Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(request.UserName) is not null)
            return Result.Fail(new List<string> { "اسم المستخدم موجود مسبقا" });

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

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
            UserType = request.UserType,
            ActivateState = request.ActivateState,
        };
        await _userManager.CreateAsync(user, request.Password);

        if (request.Password.Length <= 7)
            return Result.Fail(new List<string> { "كلمة المرور اقل من 8 " });

        var claims = new List<Claim>();
        request.Claims.ForEach(item => { item.value.ForEach(value => claims.Add(new Claim(item.type, value))); });

        var result = await _userManager.AddClaimsAsync(user, claims);

        if (!result.Succeeded)
            return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");

        await transaction.CommitAsync();
        //var message = new SendEmailRequest
        //{
        //    Subject = "إنشاء حساب",
        //    ToEmail = user.Email,
        //    html = $" تم إنشاء حساب بالفعل يمكنك استعمال هذا البريد بما يسمح لك باجراء العمليات المخصصه "
        //};
        //await _emailSender.SendEmailAsync(message);
        return user.Id;
    }

    public async Task<Result<string>> CreateUserClaims(CreateUserClaimsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        var claims = new List<Claim>();
        request.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });

        var result = await _userManager.AddClaimsAsync(user, claims);

        if (result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.FirstName = request.FirstName;
        user.FatherName = request.FatherName;
        user.LastName = request.LastName;
        user.Nationality = request.Nationality;
        user.Country = request.Country;
        user.DateOfBirth = request.DateOfBirth;
        user.Gender = request.Gender;
        user.NationalId = request.NationalId;
        user.PassportId = request.PassportId;
        user.PassportExpirationDate = request.PassportExpirationDate;
        user.placeOfIssue = request.placeOfIssue;
        user.UserType = request.UserType;
        

        

        var existingClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in existingClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        var claims = new List<Claim>();
        request.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });

        var response = await _userManager.UpdateAsync(user);
        var result = await _userManager.AddClaimsAsync(user, claims);

        if (response.Succeeded && result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserClaims(UpdateUserClaimsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");


        var existingClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in existingClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        var claims = new List<Claim>();
        request.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });
        var result = await _userManager.AddClaimsAsync(user, claims);

        if (result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserKyc(UpdateUserKycRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.FirstName = request.FirstName;
        user.FatherName = request.FatherName;
        user.LastName = request.LastName;
        user.Nationality = request.Nationality;
        user.Country = request.Country;
        user.DateOfBirth = request.DateOfBirth;
        user.Gender = request.Gender;
        user.NationalId = request.NationalId;
        user.PassportId = request.PassportId;
        user.PassportExpirationDate = request.PassportExpirationDate;
        user.placeOfIssue = request.placeOfIssue;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return "تم تعديل هوية المستخدم بنجاح ";

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserProfile(UpdateUserProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");


        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.UserType = request.UserType;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return "تم تعديل بيانات المستخدم بنجاح ";

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }
}