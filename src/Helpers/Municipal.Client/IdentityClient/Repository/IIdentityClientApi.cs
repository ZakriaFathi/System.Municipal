using Municipal.Client.IdentityClient.IdentityRequest;
using Municipal.Utils.Vm;

namespace Municipal.Client.IdentityClient.Repository;

public interface IIdentityClientApi
{
    Task<OperationResult<string>> CreateUser(CreateUsers request);
    Task<OperationResult<string>> ChangeUserState(ChangeUsersState request);
    Task<OperationResult<string>> ChangePassword(ChangePasswords request);
    Task<OperationResult<string>> UpdateUser(UpdateUsers request);
    Task<OperationResult<string>> UpdateUserKyc(UpdateUsersKyc request);
    Task<OperationResult<string>> UpdateUserProfile(UpdateUsersProfile request);
    Task<OperationResult<string>> CreateUserClaims(CreateUsersClaims request);
    Task<OperationResult<string>> UpdateUserClaims(UpdateUsersClaims request);
}