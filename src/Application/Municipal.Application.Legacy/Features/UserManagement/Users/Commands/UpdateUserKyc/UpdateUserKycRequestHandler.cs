using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserKyc;

public class UpdateUserKycRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserKycRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public UpdateUserKycRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpPost("api/UserManagement/UpdateUserKyc")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/UpdateUserKyc",
        Tags = new[] { "UserManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserKycRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.UpdateUserKycAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}