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
using Municipal.Client.IdentityClient.IdentityRequest;
using Municipal.Client.IdentityClient.Repository;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;
using Municipal.Utils.Enums;
using Municipal.Utils.Vm;

namespace Municipal.DataAccess.Repositories.UserManageRepo;

public class UserManageRepository : IUserManagmentRepository
{
     private readonly UserManagementDbContext _userManagementDb;
        private readonly IIdentityClientApi _identity;
        private readonly IPermissionsRepository _permissionsService;

        public UserManageRepository(UserManagementDbContext userManagementDb, IIdentityClientApi identity , IPermissionsRepository permissionsService = null)
        {
            _userManagementDb = userManagementDb;
            _identity = identity;
            _permissionsService = permissionsService;
        }

        public async Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest changePasswordDto, CancellationToken cancellationToken)
        {
            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == changePasswordDto.UserId);
            if (user is null)
                return Result.Fail(new List<string>() { "هذا المستخدم غير موجود" });

            if (user.Password != changePasswordDto.OldPassWord)
                return Result.Fail(new List<string>() { "كلمة المرور السابقة غير صحيحة" });

            if (changePasswordDto.NewPassWord != changePasswordDto.ConfirmNewPassWord)
                return Result.Fail(new List<string>() { "كلمة المرور غير متطابقة" });

            var request = ChangePasswords.Prepare(changePasswordDto);
            var response = await _identity.ChangePassword(request);

            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            user.Password = changePasswordDto.NewPassWord;
            var result = await _userManagementDb.SaveChangesAsync();

            if (result <= 0)
                Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");

            await transaction.CommitAsync();
            return "تم تغيير كلمة المرور بنجاح";
        }

        public async Task<Result<string>> ChangeUserActivationAsync(ChangeUserActivationRequest changeUserActivationDto, CancellationToken cancellationToken)
        {
            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();
            var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == changeUserActivationDto.UserId);
            if (user is null)
                return Result.Fail("هذا المستخدم غير موجود" );

            user.ActivateState = changeUserActivationDto.State;
            user.UpdatedAt = DateTime.Now;

            var result = await _userManagementDb.SaveChangesAsync();
            if (result <= 0)
                Result.Fail("حدثت مشكلة في الخادم الرجاء الاتصال بالدعم الفني");
            var request = ChangeUsersState.Prepare(changeUserActivationDto);
            var response = await _identity.ChangeUserState(request);

            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());
            
            await transaction.CommitAsync();

            return "تم تغيير حالة المستخدم بنجاح";
        }

        public async Task<Result<string>> CreateUserAsync(CreateUserRequest createUserDto, CancellationToken cancellationToken)
        {
            var allPermissions = await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
            var request = CreateUsers.Prepare(createUserDto, allPermissions.Value);

            var response = await _identity.CreateUser(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.UserName == createUserDto.UserName, cancellationToken: cancellationToken);
            if (user is not null)
                return Result.Fail( "اسم المستخدم موجود مسبقا" );

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync(cancellationToken);

            var newUser = new User()
            {
                Id = Guid.Parse(response.Content.ToString()),
                PhoneNumber = createUserDto.PhoneNumber,
                Email = createUserDto.Email,
                Address = createUserDto.Address,
                UserName = createUserDto.UserName,
                UserType = createUserDto.UserType,
                Password = createUserDto.Password,
                ActivateState = ActivateState.InActive,
            };
            var userKyc = new UserKyc()
            {
                UserId = Guid.Parse(response.Content.ToString()),
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
            };
            var userPermissions = createUserDto.Permissions.Select(
            permission => new UserPermission()
            {
                UserId = Guid.Parse(response.Content.ToString()),
                PermissionId = int.Parse(permission),
            }).ToList();

            await _userManagementDb.Users.AddAsync(newUser, cancellationToken);
            await _userManagementDb.UsersKyc.AddAsync(userKyc, cancellationToken);
            await _userManagementDb.UserPermissions.AddRangeAsync(userPermissions, cancellationToken);
            var result = await _userManagementDb.SaveChangesAsync(cancellationToken);

            if (result <= 0)
                return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });

            await transaction.CommitAsync(cancellationToken);

            return  "تمت عملية اضافة المستخدم بنجاح " ;
        }

        public async Task<Result<string>> CreateUserPermissionsAsync(CreateUserPermissionsRequest createUserPermissionsDto, CancellationToken cancellationToken)
        {
            var allPermissions = await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
            var request = CreateUsersClaims.Prepare(createUserPermissionsDto, allPermissions.Value);

            var response = await _identity.CreateUserClaims(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            var userPermissions = createUserPermissionsDto.Permissions.Select(
                permission => new UserPermission()
                {
                    UserId = Guid.Parse(response.Content.ToString()),
                    PermissionId = int.Parse(permission),
                }).ToList();

            var result = await _userManagementDb.SaveChangesAsync();

            if (result <= 0)
                return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });

            await transaction.CommitAsync();

            return "تمت عملية اضافة المستخدم بنجاح ";
        }

        public async Task<Result<GetUserProfileResponse>> GetUserProfileAsync(GetUserProfileRequset request, CancellationToken cancellationToken)
        {
            var userProfile = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (userProfile is null)
                return Result.Fail( "هذا المستخدم غير موجود" );

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

        public async Task<Result<GetUserKycResponse>> GetUserKycAsync(GetUserKycRequset request, CancellationToken cancellationToken)
        {
            var userKyc = await _userManagementDb.UsersKyc.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
            if (userKyc is null)
                return Result.Fail( "هذا المستخدم غير موجود" );

            var result = new GetUserKycResponse
            {
                Country = userKyc.Country,
                FirstName = userKyc.FirstName,
                FatherName = userKyc.FatherName,
                LastName = userKyc.LastName
            };

            return result;
        }

        public async Task<Result<List<GetPermissionsResponse>>> GetUserRolesAndPermissionsAsync(GetUserRolesAndPermissionsRequest request, CancellationToken cancellationToken)
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
            }).ToListAsync();

            return userRolesAndPermissions;
        }

        public async Task<Result<PageResult<GetUsersResponse>>> GetUsersAsync(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManagementDb.Users.Select(x => new GetUsersResponse()
            {
                Id = x.Id,
                UserName = x.UserName,
                ActivateState = x.ActivateState,
                Email = x.Email,
                UserType = x.UserType
            }).ToListAsync(cancellationToken); 

            
            if (users is null) return Result.Fail("لا يوجد طلبات");

            var totalCount = users.Count;
            
            return PageResult<GetUsersResponse>.Create(totalCount,request.Page, request.PageSize, users);

            // var response = PageResult<GetUsersResponse>.Create(users.Count, request.Page, request.PageSize, users.ToPageResult(request.Page).ToList());
            // return response;

        }

        public async Task<Result<string>> UpdateUserAsync(UpdateUserRequest updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == updateUserDto.Id);
            if (user is null)
                return Result.Fail("المستخدم غير موجود");    
            
            var userKyc = await _userManagementDb.UsersKyc.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (userKyc is null)
                return Result.Fail("المستخدم غير موجود");

            var deleteOldPermissions = await _permissionsService.DeleteUserPermissions(updateUserDto.Id, cancellationToken);

            var allPermissions = await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
            var request = UpdateUsers.Prepare(updateUserDto, allPermissions.Value);

            var response = await _identity.UpdateUser(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Address = updateUserDto.Address;
            user.UserType = updateUserDto.UserType;

            userKyc.FirstName = updateUserDto.FirstName;
            userKyc.FatherName = updateUserDto.FatherName;
            userKyc.LastName = updateUserDto.LastName;
            userKyc.Nationality = updateUserDto.Nationality;
            userKyc.Country = updateUserDto.Country;
            userKyc.DateOfBirth = updateUserDto.DateOfBirth;
            userKyc.Gender = updateUserDto.Gender;
            userKyc.NationalId = updateUserDto.NationalId;
            userKyc.PassportId = updateUserDto.PassportId;
            userKyc.PassportExpirationDate = updateUserDto.PassportExpirationDate;
            userKyc.placeOfIssue = updateUserDto.placeOfIssue;

            var userPermissions = updateUserDto.Permissions.Select(
            permission => new UserPermission()
            {
                UserId = Guid.Parse(response.Content.ToString()),
                PermissionId = int.Parse(permission),
            }).ToList();


            await _userManagementDb.UserPermissions.AddRangeAsync(userPermissions);
            var result = await _userManagementDb.SaveChangesAsync();

            if (result <= 0)
                return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });

            await transaction.CommitAsync();

            return "تمت عملية اضافة المستخدم بنجاح ";
        }

        public async Task<Result<string>> UpdateUserKycAsync(UpdateUserKycRequest updateUserKycDto, CancellationToken cancellationToken)
        {
            var userKyc = await _userManagementDb.UsersKyc.FirstOrDefaultAsync(x => x.UserId == updateUserKycDto.UserId);
            if (userKyc is null)
                return Result.Fail("المستخدم غير موجود");

            var request = UpdateUsersKyc.Prepare(updateUserKycDto);

            var response = await _identity.UpdateUserKyc(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            userKyc.FirstName = updateUserKycDto.FirstName;
            userKyc.FatherName = updateUserKycDto.FatherName;
            userKyc.LastName = updateUserKycDto.LastName;
            userKyc.Nationality = updateUserKycDto.Nationality;
            userKyc.Country = updateUserKycDto.Country;
            userKyc.DateOfBirth = updateUserKycDto.DateOfBirth;
            userKyc.Gender = updateUserKycDto.Gender;
            userKyc.NationalId = updateUserKycDto.NationalId;
            userKyc.PassportId = updateUserKycDto.PassportId;
            userKyc.PassportExpirationDate = updateUserKycDto.PassportExpirationDate;
            userKyc.placeOfIssue = updateUserKycDto.placeOfIssue;

            await _userManagementDb.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync();

            return "تم تعديل المشترك بنجاح";
        }

        public async Task<Result<string>> UpdateUserPermissionsAsync(UpdateUserPermissionsRequest updateUserPermissionsDto, CancellationToken cancellationToken)
        {
            var deleteOldPermissions = await _permissionsService.DeleteUserPermissions(updateUserPermissionsDto.UserId, cancellationToken);

            var allPermissions = await _permissionsService.GetAllPermissions(new GetAllPermissionsRequest(), cancellationToken);
            var request = UpdateUsersClaims.Prepare(updateUserPermissionsDto, allPermissions.Value);

            var response = await _identity.UpdateUserClaims(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            var userPermissions = updateUserPermissionsDto.Permissions.Select(
                permission => new UserPermission()
                {
                    UserId = Guid.Parse(response.Content.ToString()),
                    PermissionId = int.Parse(permission),
                }).ToList();

            await _userManagementDb.UserPermissions.AddRangeAsync(userPermissions);
            var result = await _userManagementDb.SaveChangesAsync();

            if (result <= 0)
                return Result.Fail(new List<string>() { "حدثت مشكلة بالخادم الرجاء الاتصال بالدعم الفني" });

            await transaction.CommitAsync();

            return "تمت عملية اضافة المستخدم بنجاح ";
        }

        public async Task<Result<string>> UpdateUserProfileAsync(UpdateUserProfileRequest updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _userManagementDb.Users.FirstOrDefaultAsync(x => x.Id == updateUserDto.UserId);
            if (user is null)
                return Result.Fail("المستخدم غير موجود");

            var request = UpdateUsersProfile.Prepare(updateUserDto);

            var response = await _identity.UpdateUserProfile(request);
            if (response.Type != OperationResult.ResultType.Success)
                return Result.Fail(response.Messages.ToList());

            await using var transaction = await _userManagementDb.Database.BeginTransactionAsync();

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Address = updateUserDto.Address;
            user.UserType = updateUserDto.UserType;

            await _userManagementDb.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync();

            return "تم تعديل المشترك بنجاح";

        }
}