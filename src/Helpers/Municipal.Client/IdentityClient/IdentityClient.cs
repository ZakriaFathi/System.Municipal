using Microsoft.Extensions.Options;
using Municipal.Client.IdentityClient.IdentityRequest;
using Municipal.Utils.Clients;
using Municipal.Utils.Options;
using Municipal.Utils.Vm;
using Newtonsoft.Json;

namespace Municipal.Client.IdentityClient;

public class IdentityClient
{
    private readonly HttpClient _httpClient;
    private readonly ComponentConnectivityOptions _connectivityOptions;

    public IdentityClient(HttpClient httpClient, IOptions<ComponentConnectivityOptions> connectivityOptions)
    {
        _httpClient = httpClient;
        _connectivityOptions = connectivityOptions.Value;
    }

    public async Task<OperationResult<string>> CreateUser(CreateUsers request)
    {
        try
        {
            var response = await new HttpClientBuilder(_httpClient)
                .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                    $"/api/IdentityManagement/CreateUser?culture=ar-LY")
                .WithMethod(HttpMethodType.Post)
                .WithContent(JsonConvert.SerializeObject(request))
                .Send<OperationResult<string>>();

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<OperationResult<string>> ChangePassword(ChangePasswords request)
    {

        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/ChangePassword?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }

    public async Task<OperationResult<string>> ChangeUserState(ChangeUsersState request)
    {
        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/ChangeUserActivation?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }

    public async Task<OperationResult<string>> UpdateUser(UpdateUsers request)
    {
        try
        {
            var response = await new HttpClientBuilder(_httpClient)
                .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                    $"/api/IdentityManagement/UpdateUser?culture=ar-LY")
                .WithMethod(HttpMethodType.Post)
                .WithContent(JsonConvert.SerializeObject(request))
                .Send<OperationResult<string>>();

            return response;
        }
        catch (Exception e)
        {
            return OperationResult<string>.UnValid(e.Message);

        }
    }

    public async Task<OperationResult<string>> UpdateUserKyc(UpdateUsersKyc request)
    {
        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/UpdateUserKyc?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }

    public async Task<OperationResult<string>> UpdateUserProfile(UpdateUsersProfile request)
    {
        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/UpdateUserProfile?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }

    public async Task<OperationResult<string>> CreateUserClaims(CreateUsersClaims request)
    {
        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/CreateUserClaims?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }

    public async Task<OperationResult<string>> UpdateUserClaims(UpdateUsersClaims request)
    {
        var response = await new HttpClientBuilder(_httpClient)
            .WithUrl(_connectivityOptions.LinkOptions.Single(x => x.LinkKey == "Identity").Link,
                $"/api/IdentityManagement/UpdateUserClaims?culture=ar-LY")
            .WithMethod(HttpMethodType.Post)
            .WithContent(JsonConvert.SerializeObject(request))
            .Send<OperationResult<string>>();

        return response;
    }
}