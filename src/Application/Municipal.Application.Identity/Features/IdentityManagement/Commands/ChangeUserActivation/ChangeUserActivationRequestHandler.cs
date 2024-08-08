using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangeUserActivation;

public class ChangeUserActivationRequestHandler : EndpointBase.WithRequestWithResult<ChangeUserActivationCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;
    public ChangeUserActivationRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/ChangeUserActivation")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/ChangeUserActivation",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]ChangeUserActivationCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.ChangeUserActivation(command, cancellationToken);
        return responce.ToOperationResult(); 
    }
}