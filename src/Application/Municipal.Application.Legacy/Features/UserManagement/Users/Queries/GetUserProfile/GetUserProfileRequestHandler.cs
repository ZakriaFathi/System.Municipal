using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;

public class GetUserProfileRequestHandler : EndpointBase.WithRequestWithResult<GetUserProfileRequset, OperationResult<GetUserProfileResponse>>
{
    private readonly IUserManagmentRepository _userManagment;

    public GetUserProfileRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpGet("api/UserManagement/GetUserProfile")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetUserProfile",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<GetUserProfileResponse>> HandleAsync([FromQuery] GetUserProfileRequset request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.GetUserProfileAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}