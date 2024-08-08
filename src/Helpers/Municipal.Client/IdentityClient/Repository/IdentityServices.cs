using Municipal.Client.IdentityClient.IdentityRequest;
using Municipal.Utils.Vm;

namespace Municipal.Client.IdentityClient.Repository;

public class IdentityServices : IIdentityClientApi
{
    private readonly IdentityClient _identityClient;

    public IdentityServices(IdentityClient identityClient)
    {
        _identityClient = identityClient;
    }

    public async Task<OperationResult<string>> ChangePassword(ChangePasswords request)
        => await _identityClient.ChangePassword(request);


    public async Task<OperationResult<string>> ChangeUserState(ChangeUsersState request)
        => await _identityClient.ChangeUserState(request);

    public async Task<OperationResult<string>> CreateUser(CreateUsers request)
        => await _identityClient.CreateUser(request);

    public async Task<OperationResult<string>> CreateUserClaims(CreateUsersClaims request)
        => await _identityClient.CreateUserClaims(request);

    public async Task<OperationResult<string>> UpdateUser(UpdateUsers request)
        => await _identityClient.UpdateUser(request);

    public async Task<OperationResult<string>> UpdateUserClaims(UpdateUsersClaims request)
        => await _identityClient.UpdateUserClaims(request);

    public async Task<OperationResult<string>> UpdateUserKyc(UpdateUsersKyc request)
        => await _identityClient.UpdateUserKyc(request);

    public async Task<OperationResult<string>> UpdateUserProfile(UpdateUsersProfile request)
        => await _identityClient.UpdateUserProfile(request);
}