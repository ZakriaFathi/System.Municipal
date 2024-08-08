using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserClaims;

public class UpdateUserClaimsRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserClaimsCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;

    public UpdateUserClaimsRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/UpdateUserClaims")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserClaims",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserClaimsCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.UpdateUserClaims(command, cancellationToken);
        return responce.ToOperationResult();
    }
}