using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserKyc;

public class GetUserKycRequestHandler : EndpointBase.WithRequestWithResult<GetUserKycRequset, OperationResult<GetUserKycResponse>>
{
    private readonly IUserManagmentRepository _userManagment;

    public GetUserKycRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }
    [HttpGet("api/UserManagement/GetUserKyc")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/GetUserKyc",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<GetUserKycResponse>> HandleAsync([FromQuery]GetUserKycRequset request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.GetUserKycAsync(request, cancellationToken);
        return responce.ToOperationResult();    }
}