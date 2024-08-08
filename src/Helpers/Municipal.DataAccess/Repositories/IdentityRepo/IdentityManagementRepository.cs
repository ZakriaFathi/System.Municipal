using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangePassword;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangeUserActivation;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUser;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUserClimas;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUser;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserClaims;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserKyc;
using Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserProfile;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Models;

namespace Municipal.DataAccess.Repositories.IdentityRepo;

public class IdentityManagementRepository: IIdentityManagementRepository
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

    public async Task<Result<string>> ChangePassword(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        var password = await _userManager.CheckPasswordAsync(user, command.OldPassword);
        if (password == false)
            return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

        var result = await _userManager.ChangePasswordAsync(user, command.OldPassword, command.NewPassword);
        if (result.Succeeded)
            return "تم تغيير كلمة المرور بنجاح";

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> ChangeUserActivation(ChangeUserActivationCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.ActivateState = command.State;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ?
            "تم تغيير حالة المستخدم بنجاح" :
            Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(command.UserName) is not null)
            return Result.Fail(new List<string> { "اسم المستخدم موجود مسبقا" });

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var user = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            FatherName = command.FatherName,
            Nationality = command.Nationality,
            Country = command.Country,
            DateOfBirth = command.DateOfBirth,
            Gender = command.Gender,
            NationalId = command.NationalId,
            PassportId = command.PassportId,
            PassportExpirationDate = command.PassportExpirationDate,
            placeOfIssue = command.placeOfIssue,
            PhoneNumber = command.PhoneNumber,
            Email = command.Email,
            Address = command.Address,
            UserName = command.UserName,
            UserType = command.UserType,
            ActivateState = command.ActivateState,
        };
        await _userManager.CreateAsync(user, command.Password);

        if (command.Password.Length <= 7)
            return Result.Fail(new List<string> { "كلمة المرور اقل من 8 " });

        var claims = new List<Claim>();
        command.Claims.ForEach(item => { item.value.ForEach(value => claims.Add(new Claim(item.type, value))); });

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

    public async Task<Result<string>> CreateUserClaims(CreateUserClaimsCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        var claims = new List<Claim>();
        command.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });

        var result = await _userManager.AddClaimsAsync(user, claims);

        if (result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.UserName = command.UserName;
        user.Email = command.Email;
        user.PhoneNumber = command.PhoneNumber;
        user.Address = command.Address;
        user.FirstName = command.FirstName;
        user.FatherName = command.FatherName;
        user.LastName = command.LastName;
        user.Nationality = command.Nationality;
        user.Country = command.Country;
        user.DateOfBirth = command.DateOfBirth;
        user.Gender = command.Gender;
        user.NationalId = command.NationalId;
        user.PassportId = command.PassportId;
        user.PassportExpirationDate = command.PassportExpirationDate;
        user.placeOfIssue = command.placeOfIssue;
        user.UserType = command.UserType;
        

        

        var existingClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in existingClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        var claims = new List<Claim>();
        command.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });

        var response = await _userManager.UpdateAsync(user);
        var result = await _userManager.AddClaimsAsync(user, claims);

        if (response.Succeeded && result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserClaims(UpdateUserClaimsCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);

        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");


        var existingClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in existingClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        var claims = new List<Claim>();
        command.Claims.ForEach(item =>
        {
            item.value.ForEach(value => claims.Add(new Claim(item.type, value)));

        });
        var result = await _userManager.AddClaimsAsync(user, claims);

        if (result.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserKyc(UpdateUserKycCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");

        user.FirstName = command.FirstName;
        user.FatherName = command.FatherName;
        user.LastName = command.LastName;
        user.Nationality = command.Nationality;
        user.Country = command.Country;
        user.DateOfBirth = command.DateOfBirth;
        user.Gender = command.Gender;
        user.NationalId = command.NationalId;
        user.PassportId = command.PassportId;
        user.PassportExpirationDate = command.PassportExpirationDate;
        user.placeOfIssue = command.placeOfIssue;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return "تم تعديل هوية المستخدم بنجاح ";

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateUserProfile(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
            return Result.Fail("هذا المستخدم غير موجود");


        user.UserName = command.UserName;
        user.Email = command.Email;
        user.PhoneNumber = command.PhoneNumber;
        user.Address = command.Address;
        user.UserType = command.UserType;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return "تم تعديل بيانات المستخدم بنجاح ";

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }
}