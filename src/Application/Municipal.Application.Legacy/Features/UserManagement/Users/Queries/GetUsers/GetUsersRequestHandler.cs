using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUsers;

public class GetUsersRequestHandler : EndpointBase.WithRequestWithResult<GetUsersRequest, OperationResult<PageResult<GetUsersResponse>>>
{
    private readonly IUserManagmentRepository _userManagment;

    public GetUsersRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpGet("api/UserManagement/GetUsers")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetUsers",
        Tags = new[] { "UserManagement" }
    )]

    public override async Task<OperationResult<PageResult<GetUsersResponse>>> HandleAsync([FromQuery] GetUsersRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.GetUsersAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}