using System.Security.Claims;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Permissions.Queries.GetAllPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUser;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserProfile;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserRoles;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUsers;
using Municipal.Application.Legacy.Models.IdentityModel;
using Municipal.Application.Legacy.Models.UserManagement;
using Municipal.Client.IdentityClient.IdentityRequest;
using Municipal.Client.IdentityClient.Repository;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;
using Municipal.Domin.Models;
using Municipal.Utils.Enums;
using Municipal.Utils.Vm;

namespace Municipal.DataAccess.Repositories.UserManageRepo;

public class UserManageRepository : IUserManagmentRepository
{ 
    private readonly UserManagementDbContext _userManagementDb; 
    private readonly ISherdUserRepository _sherdUserRepository; 
    private readonly IPermissionsRepository _permissionsService;

    public UserManageRepository(UserManagementDbContext userManagementDb,
        IPermissionsRepository permissionsService, ISherdUserRepository sherdUserRepository)
    {
        _userManagementDb = userManagementDb;
        _permissionsService = permissionsService;
        _sherdUserRepository = sherdUserRepository;
    }

    public async Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest changePasswordDto,
        CancellationToken cancellationToken)
    { 
        var userPassword = await _sherdUserRepository.ChangePassword(new ChangePassword()
        {
            UserId = changePasswordDto.UserId,
            OldPassWord = changePasswordDto.OldPassWord,
            NewPassWord = changePasswordDto.NewPassWord,
            ConfirmNewPassWord = changePasswordDto.ConfirmNewPassWord
        }, cancellationToken);
        
        if (!userPassword.IsSuccess)
            return Result.Fail(userPassword.Errors.ToList());
        
        var identityPassword = await _sherdUserRepository.ChangeIdentityPassword(new ChangeIdentityPassword()
        {
            UserId = changePasswordDto.UserId.ToString(),
            OldPassword = changePasswordDto.OldPassWord,
            NewPassword = changePasswordDto.NewPassWord
        }, cancellationToken);
        
        if (!identityPassword.IsSuccess)
            return Result.Fail(identityPassword.Errors.ToList());
        
        return "تم تغيير كلمة المرور بنجاح";
    }

    public async Task<Result<string>> ChangeUserActivationAsync(ChangeUserActivationRequest changeUserActivationDto,
        CancellationToken cancellationToken)
    { 
        var changeUserActivation =
            await _sherdUserRepository.ChangeUserActivation(new ChangeUserActivation()
            {
                UserId = changeUserActivationDto.UserId,
                State = changeUserActivationDto.State
            }, cancellationToken);
        
        if (!changeUserActivation.IsSuccess)
            return Result.Fail(changeUserActivation.Errors.ToList());


        var changeIdentityActivation = await _sherdUserRepository.ChangeIdentityActivation(new ChangeIdentityActivation()
        {
            UserId = changeUserActivationDto.UserId.ToString(),
            State = changeUserActivationDto.State
        }, cancellationToken);
        
        if (!changeIdentityActivation.IsSuccess)
            return Result.Fail(changeIdentityActivation.Errors.ToList());
        
        return "تم تغيير حالة المستخدم بنجاح";
    }

    public async Task<Result<string>> CreateUserAsync(CreateUserRequest createUserDto,
        CancellationToken cancellationToken)
    { 
        var user = await _sherdUserRepository.GetIdentityUserByUserName(createUserDto.UserName, cancellationToken);
        
        if (!user.IsSuccess)
            return Result.Fail(user.Errors.ToList());
        
        await using var transaction = await _userManagementDb.Database.BeginTransactionAsync(cancellationToken);

        var identityUser = await _sherdUserRepository.InsertIdentityUser(new InsertAndUpdateIdentityUser()
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            FatherName = createUserDto.FatherName,
            Nationality = createUserDto.Nationality,
            Country = createUserDto.Country,
            DateOfBirth = createUserDto.DateOfBirth,
            Gender = createUserDto.Gender,
            NationalId = createUserDto.NationalId,
            PassportId = createUserDto.PassportId,
            PassportExpirationDate = createUserDto.PassportExpirationDate,
            placeOfIssue = createUserDto.placeOfIssue,
            PhoneNumber = createUserDto.PhoneNumber,
            Email = createUserDto.Email,
            Address = createUserDto.Address,
            UserName = createUserDto.UserName,
            UserType = createUserDto.UserType,
            Password = createUserDto.Password,
            ActivateState = ActivateState.InActive,
        }, cancellationToken);
        
        if (!identityUser.IsSuccess)
            return Result.Fail(identityUser.Errors.ToList());
        
        var userProfile = await _sherdUserRepository.InsertUser(new InsertAndUpdateUserProfile()
        {
            UserId = Guid.Parse(identityUser.Value),
            PhoneNumber = createUserDto.PhoneNumber,
            Email = createUserDto.Email,
            Address = createUserDto.Address,
            UserName = createUserDto.UserName,
            UserType = createUserDto.UserType,
            Password = createUserDto.Password,
        }, cancellationToken);
        
        if (!userProfile.IsSuccess)
            return Result.Fail(userProfile.Errors.ToList());
        
        var userKyc = await _sherdUserRepository.InsertUserKyc(new InsertAndUpdateUserKyc()
        {
            UserId = Guid.Parse(identityUser.Value),
            FirstName = createUserDto.FirstName,
            FatherName = createUserDto.FatherName,
            LastName = createUserDto.LastName,
            Nationality = createUserDto.Nationality,
            Country = createUserDto.Country,
            DateOfBirth = createUserDto.DateOfBirth,
            Gender = createUserDto.Gender,
            NationalId = createUserDto.NationalId,
            PassportId = createUserDto.PassportId,
            PassportExpirationDate = createUserDto.PassportExpirationDate,
            placeOfIssue = createUserDto.placeOfIssue
        }, cancellationToken);
        
        if (!userKyc.IsSuccess)
            return Result.Fail(userKyc.Errors.ToList());

        var cliams = await CreateUserPermissionsAsync(new CreateUserPermissionsRequest()
        {
            UserId = identityUser.Value,
            Permissions = createUserDto.Permissions
        }, cancellationToken);
        
        if (!cliams.IsSuccess)
            return Result.Fail(cliams.Errors.ToList());
        
        await transaction.CommitAsync(cancellationToken);

        return "تمت عملية اضافة المستخدم بنجاح ";
    }

    public async Task<Result<string>> CreateUserPermissionsAsync(CreateUserPermissionsRequest createUserPermissionsDto,
        CancellationToken cancellationToken)
    {
                
        var allPermissions =
            await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
        
        if (!allPermissions.IsSuccess)
            return Result.Fail(allPermissions.Errors.ToList());
        
        
        var claim = new List<Claim>();
        foreach (var t in allPermissions.Value)
        {
            t.Permissions.ForEach(x =>
            {
                createUserPermissionsDto.Permissions.ForEach(y =>
                {
                    if (y.ToString() == x.PermissionId.ToString())
                        claim.Add(new Claim(x.PermissionName, t.RoleName));
                });
            });
        }
        var claims = claim.GroupBy(x => x.Type).Select(y => new UserClaims 
        { 
            type = y.Key, 
            value = y.Select(x => x.Value).ToList() 
        }).ToList();
        
        var identityClims = await _sherdUserRepository.InsertIdentityUserClaims(new InsertAndUpdateIdentityClaims()
        {
            UserId = createUserPermissionsDto.UserId,
            Claims = claims
        }, cancellationToken);
        
        if (!identityClims.IsSuccess)
            return Result.Fail(identityClims.Errors.ToList());

        var userClims = await _sherdUserRepository.CreateUserPermissions(new InsertAndUpdateUserPermissions()
        {
            UserId =   createUserPermissionsDto.UserId,
            Permissions = createUserPermissionsDto.Permissions,
        }, cancellationToken);
        
        if (!userClims.IsSuccess)
            return Result.Fail(userClims.Errors.ToList());

        return " تمت اضافة صلاحيات المستخدم بنجاح ";
    }

    public async Task<Result<GetUserProfileResponse>> GetUserProfileAsync(GetUserProfileRequset request,
        CancellationToken cancellationToken)
    {
        var userProfile =
            await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (userProfile is null)
            return Result.Fail("هذا المستخدم غير موجود");

        var result = new GetUserProfileResponse
        {
            Id = request.UserId,
            Address = userProfile.Address,
            Email = userProfile.Email,
            PhoneNumber = userProfile.PhoneNumber,
            UserName = userProfile.UserName,
            UserType = userProfile.UserType.ToString("D")
        };

        return result;
    }

    public async Task<Result<GetUserKycResponse>> GetUserKycAsync(GetUserKycRequset request,
        CancellationToken cancellationToken)
    {
        var userKyc =
            await _userManagementDb.UsersKyc.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        if (userKyc is null)
            return Result.Fail("هذا المستخدم غير موجود");

        var result = new GetUserKycResponse
        {
            Country = userKyc.Country,
            FirstName = userKyc.FirstName,
            FatherName = userKyc.FatherName,
            LastName = userKyc.LastName
        };

        return result;
    }

    public async Task<Result<List<GetPermissionsResponse>>> GetUserRolesAndPermissionsAsync(
        GetUserRolesAndPermissionsRequest request, CancellationToken cancellationToken)
    {
        var userRolesAndPermissions = await _userManagementDb.UserPermissions
            .Include(up => up.Permission)
            .ThenInclude(r => r.Role)
            .Where(x => x.UserId == request.UserId)
            .GroupBy(x => x.Permission.RoleId)
            .Select(g => new GetPermissionsResponse()
            {
                RoleName = g.First(x => x.Permission.Role.Id == x.Permission.RoleId).Permission.Role.Name,
                Permissions = g.Select(up => new
                    Permissions()
                    {
                        PermissionId = up.PermissionId,
                        PermissionName = up.Permission.Name
                    }).ToList()
            }).ToListAsync(cancellationToken);
        
        if (userRolesAndPermissions.Count <= 0)
            return Result.Fail<List<GetPermissionsResponse>>( "لا يوجد صلاحيات للمستخدم" );

        return userRolesAndPermissions;
    }

    public async Task<Result<PageResult<GetUsersResponse>>> GetUsersAsync(GetUsersRequest request,
        CancellationToken cancellationToken)
    {
        var users = await _userManagementDb.Users.Select(x => new GetUsersResponse()
        {
            Id = x.Id,
            UserName = x.UserName,
            ActivateState = x.ActivateState,
            Email = x.Email,
            UserType = x.UserType
        }).ToListAsync(cancellationToken);


        if (users.Count <= 0) return Result.Fail("لا يوجد مستخدمين");

        var totalCount = users.Count;

        var response = PageResult<GetUsersResponse>.Create(totalCount, request.Page, request.PageSize,
            users.ToPageResult(request.Page).ToList());

        return response;
    }

    public async Task<Result<string>> UpdateUserAsync(UpdateUserRequest updateUserDto,
        CancellationToken cancellationToken)
    {
        var user = await _sherdUserRepository.GetUserById(updateUserDto.UserId, cancellationToken);
        
        if (!user.IsSuccess)
            return Result.Fail(user.Errors.ToList()); 
        
        var userKyc = await _sherdUserRepository.GetUserById(user.Value.Id, cancellationToken);
        
        if (!userKyc.IsSuccess)
            return Result.Fail(userKyc.Errors.ToList());

        var updateIdentityUser = await _sherdUserRepository.UpdateIdentityUser(new InsertAndUpdateIdentityUser()
        {
            UserName = updateUserDto.UserName,
            Email = updateUserDto.Email,
            PhoneNumber = updateUserDto.PhoneNumber,
            Address = updateUserDto.Address,
            FirstName = updateUserDto.FirstName,
            FatherName = updateUserDto.FatherName,
            LastName = updateUserDto.LastName,
            Nationality = updateUserDto.Nationality,
            Country = updateUserDto.Country,
            DateOfBirth = updateUserDto.DateOfBirth,
            Gender = updateUserDto.Gender,
            NationalId = updateUserDto.NationalId,
            PassportId = updateUserDto.PassportId,
            PassportExpirationDate = updateUserDto.PassportExpirationDate,
            placeOfIssue = updateUserDto.placeOfIssue,
            UserType = updateUserDto.UserType,
        }, cancellationToken);
        
        if (!updateIdentityUser.IsSuccess)
            return Result.Fail(updateIdentityUser.Errors.ToList());
        
        var updateUserProfile = await _sherdUserRepository.UpdateUserProfile(new InsertAndUpdateUserProfile()
        {
            UserName = updateUserDto.UserName,
            Email = updateUserDto.Email,
            PhoneNumber = updateUserDto.PhoneNumber,
            Address = updateUserDto.Address,
            UserType = updateUserDto.UserType,
        }, cancellationToken);
        
        if (!updateUserProfile.IsSuccess)
            return Result.Fail(updateUserProfile.Errors.ToList());
        
        var updateUserKyc = await _sherdUserRepository.UpdateUserKyc(new InsertAndUpdateUserKyc()
        {
            FirstName = updateUserDto.FirstName,
            FatherName = updateUserDto.FatherName,
            LastName = updateUserDto.LastName,
            Nationality = updateUserDto.Nationality,
            Country = updateUserDto.Country,
            DateOfBirth = updateUserDto.DateOfBirth,
            Gender = updateUserDto.Gender,
            NationalId = updateUserDto.NationalId,
            PassportId = updateUserDto.PassportId,
            PassportExpirationDate = updateUserDto.PassportExpirationDate,
            placeOfIssue = updateUserDto.placeOfIssue,
        }, cancellationToken);
        
        if (!updateUserKyc.IsSuccess)
            return Result.Fail(updateUserKyc.Errors.ToList());
        
        var updateCliams = await UpdateUserPermissionsAsync(new UpdateUserPermissionsRequest()
        {
            UserId = updateUserDto.UserId,
            Permissions = updateUserDto.Permissions
        }, cancellationToken);
        
        if (!updateCliams.IsSuccess)
            return Result.Fail(updateCliams.Errors.ToList());

        return "تمت عملية تعديل المستخدم بنجاح ";
    }

    public async Task<Result<string>> UpdateUserKycAsync(UpdateUserKycRequest updateUserKycDto,
        CancellationToken cancellationToken)
    {
        var updateUserKyc = await _sherdUserRepository.UpdateUserKyc(new InsertAndUpdateUserKyc()
        {
            UserId = updateUserKycDto.UserId,
            FirstName = updateUserKycDto.FirstName,
            FatherName = updateUserKycDto.FatherName,
            LastName = updateUserKycDto.LastName,
            Nationality = updateUserKycDto.Nationality,
            Country = updateUserKycDto.Country,
            DateOfBirth = updateUserKycDto.DateOfBirth,
            Gender = updateUserKycDto.Gender,
            NationalId = updateUserKycDto.NationalId,
            PassportId = updateUserKycDto.PassportId,
            PassportExpirationDate = updateUserKycDto.PassportExpirationDate,
            placeOfIssue = updateUserKycDto.placeOfIssue,
        }, cancellationToken);
        
        var updateIdentityKcy = await _sherdUserRepository.UpdateIdentityUserKyc(new UpdateIdentityKyc()
        {
            UserId = updateUserKycDto.UserId.ToString(),
            FirstName = updateUserKycDto.FirstName,
            FatherName = updateUserKycDto.FatherName,
            LastName = updateUserKycDto.LastName,
            Nationality = updateUserKycDto.Nationality,
            Country = updateUserKycDto.Country,
            DateOfBirth = updateUserKycDto.DateOfBirth,
            Gender = updateUserKycDto.Gender,
            NationalId = updateUserKycDto.NationalId,
            PassportId = updateUserKycDto.PassportId,
            PassportExpirationDate = updateUserKycDto.PassportExpirationDate,
            placeOfIssue = updateUserKycDto.placeOfIssue,
        }, cancellationToken);
        
        if (!updateUserKyc.IsSuccess)
            return Result.Fail(updateUserKyc.Errors.ToList());

        return "تم تعديل هوية المستخدم بنجاح";
    }

    public async Task<Result<string>> UpdateUserPermissionsAsync(UpdateUserPermissionsRequest updateUserPermissionsDto,
        CancellationToken cancellationToken)
    {
        var deleteOldPermissions = await _permissionsService.DeleteUserPermissions(updateUserPermissionsDto.UserId, cancellationToken);
        
        if (!deleteOldPermissions.IsSuccess)
            return Result.Fail(deleteOldPermissions.Errors.ToList());
        
        var allPermissions =
            await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
        
        if (!allPermissions.IsSuccess)
            return Result.Fail(allPermissions.Errors.ToList());
        
        
        var claim = new List<Claim>();
        foreach (var t in allPermissions.Value)
        {
            t.Permissions.ForEach(x =>
            {
                updateUserPermissionsDto.Permissions.ForEach(y =>
                {
                    if (y.ToString() == x.PermissionId.ToString())
                        claim.Add(new Claim(x.PermissionName, t.RoleName));
                });
            });
        }
        var claims = claim.GroupBy(x => x.Type).Select(y => new UserClaims 
        { 
            type = y.Key, 
            value = y.Select(x => x.Value).ToList() 
        }).ToList();
        
        var identityClims = await _sherdUserRepository.UpdateIdentityUserClaims(new InsertAndUpdateIdentityClaims()
        {
            UserId = updateUserPermissionsDto.UserId.ToString(),
            Claims = claims
        }, cancellationToken);
        
        if (!identityClims.IsSuccess)
            return Result.Fail(identityClims.Errors.ToList());

        var userClims = await _sherdUserRepository.UpdateUserPermissions(new InsertAndUpdateUserPermissions()
        {
            UserId =   updateUserPermissionsDto.UserId.ToString(),
            Permissions = updateUserPermissionsDto.Permissions,
        }, cancellationToken);
        
        if (!userClims.IsSuccess)
            return Result.Fail(userClims.Errors.ToList());

        return " تمت تعديل صلاحيات المستخدم بنجاح ";
    }

    public async Task<Result<string>> UpdateUserProfileAsync(UpdateUserProfileRequest updateUserDto,
        CancellationToken cancellationToken)
    {
        var updateUserProfile =
            await _sherdUserRepository.UpdateUserProfile(new InsertAndUpdateUserProfile()
            {
                UserId = updateUserDto.UserId,
                UserName = updateUserDto.UserName,
                Email = updateUserDto.Email,
                PhoneNumber = updateUserDto.PhoneNumber,
                Address = updateUserDto.Address,
                UserType = updateUserDto.UserType
            }, cancellationToken);
        
        if (!updateUserProfile.IsSuccess)
            return Result.Fail(updateUserProfile.Errors.ToList());
        
        var updateIdentityProfile = await _sherdUserRepository.UpdateIdentityUserProfile(new UpdateIdentityProfile()
        {
            UserId = updateUserDto.UserId.ToString(),
            UserName = updateUserDto.UserName,
            Email = updateUserDto.Email,
            PhoneNumber = updateUserDto.PhoneNumber,
            Address = updateUserDto.Address,
            UserType = updateUserDto.UserType,
        }, cancellationToken);

        if (!updateIdentityProfile.IsSuccess)
            return Result.Fail(updateIdentityProfile.Errors.ToList());
        
        return "تم تعديل بيانات المستخدم بنجاح";

    }
}