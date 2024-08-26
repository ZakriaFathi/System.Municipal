

using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Models.IdentityModel;
using Municipal.Application.Legacy.Models.UserManagement;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;
using Municipal.Domin.Models;
using Municipal.Utils.Enums;

namespace Municipal.DataAccess.Repositories;

public class SherdUserRepository : ISherdUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly UserManagementDbContext _userManagementDb; 
    private readonly IMailRepository _emailSender;

    public SherdUserRepository(UserManager<AppUser> userManager, IMailRepository emailSender, UserManagementDbContext userManagementDb)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _userManagementDb = userManagementDb;
    }


    #region Identity Users

    public async Task<Result<AppUser>> GetIdentityUserById(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        return user;
    }

    public async Task<Result<AppUser>> GetIdentityUserByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userName);
        if (user == null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        return user;    
    }

    public async Task<Result<string>> InsertIdentityUser(InsertAndUpdateIdentityUser command, CancellationToken cancellationToken)
    {
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
            TwoFactorEnabled = true,
            EmailConfirmed = true
        };
        
        if (command.Password.Length <= 7)
            return Result.Fail(new List<string> { "كلمة المرور اقل من 8 " });
        
        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
            return user.Id;
        
        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateIdentityUser(InsertAndUpdateIdentityUser command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;
         
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
        
        var response = await _userManager.UpdateAsync(user);
        
        if (response.Succeeded)
            return user.Id;

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> UpdateIdentityUserProfile(UpdateIdentityProfile command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;
        
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

    public async Task<Result<string>> UpdateIdentityUserKyc(UpdateIdentityKyc command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;
        
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

        return Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");    }

    public async Task<Result<string>> ChangeIdentityPassword(ChangeIdentityPassword command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;
        
        var password = await _userManager.CheckPasswordAsync(user, command.OldPassword);
        if (password == false)
            return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

        var result = await _userManager.ChangePasswordAsync(user, command.OldPassword, command.NewPassword);
        if (result.Succeeded)
            return "تم تغيير كلمة المرور بنجاح";

        return Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> ChangeIdentityActivation(ChangeIdentityActivation command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;    
        
        user.ActivateState = command.State;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ?
            "تم تغيير حالة المستخدم بنجاح" :
            Result.Fail("حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني");
    }

    public async Task<Result<string>> InsertIdentityUserClaims(InsertAndUpdateIdentityClaims command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;   
        
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

    public async Task<Result<string>> UpdateIdentityUserClaims(InsertAndUpdateIdentityClaims command, CancellationToken cancellationToken)
    {
        var identity = await GetIdentityUserById(command.UserId, cancellationToken);
        if (identity.IsFailed)
            return Result.Fail(identity.Errors.ToList());
        
        var user = identity.Value;
    
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
    

    #endregion

    #region User Management
    
    public async Task<Result<User>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == userId,
            cancellationToken);
        if (user is null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        return user;
    }

    public async Task<Result<User>> GetUserByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.UserName == userName,
            cancellationToken);
        if (user is null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        return user;    
    }

    public async Task<Result<UserKyc>> GetUserKycById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManagementDb.UsersKyc.FirstOrDefaultAsync(x => x.UserId == userId,
            cancellationToken);
        if (user is null)
            return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

        return user;    
    }

    public async Task<Result<string>> InsertUser(InsertAndUpdateUserProfile request,
        CancellationToken cancellationToken)
    {
        var newUser = new User()
        {
            Id = Guid.Parse(request.UserId.ToString()),
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address,
            UserName = request.UserName,
            UserType = request.UserType,
            Password = request.Password,
            ActivateState = ActivateState.InActive,
        };
        await _userManagementDb.Users.AddAsync(newUser, cancellationToken);
        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);
        
        if (result <= 0)
            return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });
        
        return newUser.Id.ToString();
    }

    public async Task<Result<string>> InsertUserKyc(InsertAndUpdateUserKyc request, CancellationToken cancellationToken)
    {
        var userKyc = new UserKyc()
        {
            UserId = Guid.Parse(request.UserId.ToString()),
            FirstName = request.FirstName,
            FatherName = request.FatherName,
            LastName = request.LastName,
            Nationality = request.Nationality,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            NationalId = request.NationalId,
            PassportId = request.PassportId,
            PassportExpirationDate = request.PassportExpirationDate,
            placeOfIssue = request.placeOfIssue
        };
        
        await _userManagementDb.UsersKyc.AddAsync(userKyc, cancellationToken);
        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });
        
        return "تمت عملية اضافة المستخدم بنجاح ";
    }

    public async Task<Result<string>> ChangePassword(ChangePassword request, CancellationToken cancellationToken)
    {
        var userProfile = await GetUserById(request.UserId, cancellationToken);
        if (userProfile.Value is null)
            return Result.Fail(userProfile.Errors.ToList());
        
        var user = userProfile.Value;
        
        if (user.Password != request.OldPassWord)
            return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

        if (request.NewPassWord != request.ConfirmNewPassWord)
            return Result.Fail(new List<string>() { "كلمة المرور غير متطابقة" });
        
        user.Password = request.NewPassWord;
        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
       
        return "تم تغيير كلمة المرور بنجاح";
    }

    public async Task<Result<string>> ChangeUserActivation(ChangeUserActivation request, CancellationToken cancellationToken)
    {
        var userProfile = await GetUserById(request.UserId, cancellationToken);
        if (userProfile.Value is null)
            return Result.Fail(userProfile.Errors.ToList());
        
        var user = userProfile.Value;    
        
        user.ActivateState = request.State;
        user.UpdatedAt = DateTime.Now;

        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
        
        return "تم تغيير حالة المستخدم بنجاح";
    }

    public async Task<Result<string>> UpdateUserProfile(InsertAndUpdateUserProfile request, CancellationToken cancellationToken)
    {
        var userProfile = await GetUserById(request.UserId, cancellationToken);
        if (userProfile.Value is null)
            return Result.Fail(userProfile.Errors.ToList());
        
        var user = userProfile.Value;    

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.UserType = request.UserType;

        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
        
        return "تم تعديل المشترك بنجاح";
    }

    public async Task<Result<string>> UpdateUserKyc(InsertAndUpdateUserKyc request, CancellationToken cancellationToken)
    {
        var userKyc = await GetUserKycById(request.UserId, cancellationToken);
        if (userKyc.Value is null)
            return Result.Fail(userKyc.Errors.ToList());
        
        var user = userKyc.Value;      
        
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

        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
        
        return "تم تعديل المشترك بنجاح";
    }

    public async Task<Result<string>> CreateUserPermissions(InsertAndUpdateUserPermissions request, CancellationToken cancellationToken)
    {
        var userPermissions = request.Permissions.Select(
            permission => new UserPermission()
            {
                UserId = Guid.Parse(request.UserId),
                PermissionId = int.Parse(permission),
            }).ToList();

        await _userManagementDb.UserPermissions.AddRangeAsync(userPermissions, cancellationToken);
        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });
        
        return "تمت عملية اضافة المستخدم بنجاح ";    }

    public async Task<Result<string>> UpdateUserPermissions(InsertAndUpdateUserPermissions request, CancellationToken cancellationToken)
    {
        var userPermissions = request.Permissions.Select(
            permission => new UserPermission()
            {
                UserId = Guid.Parse(request.UserId),
                PermissionId = int.Parse(permission),
            }).ToList();

        await _userManagementDb.UserPermissions.AddRangeAsync(userPermissions, cancellationToken);
        var result = await _userManagementDb.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });
        
        return "تمت عملية اضافة المستخدم بنجاح ";    }
    
    #endregion
    
}