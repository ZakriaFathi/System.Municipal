using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;

public class ChangeUserActivationRequestHandler : EndpointBase.WithRequestWithResult<ChangeUserActivationRequest, OperationResult<string>>
{
    private readonly IUserManagmentRepository _userManagment;

    public ChangeUserActivationRequestHandler(IUserManagmentRepository userManagment)
    {
        _userManagment = userManagment;
    }

    [HttpPost("api/UserManagement/ChangeUserActivation")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "UserManagement/ChangeUserActivation",
        Tags = new[] { "UserManagement" }
    )]

    public override async Task<OperationResult<string>> HandleAsync([FromBody] ChangeUserActivationRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagment.ChangeUserActivationAsync(request, cancellationToken);
        return responce.ToOperationResult();
    }
}