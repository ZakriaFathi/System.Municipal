using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUserClimas;

public class CreateUserClimasHandler : EndpointBase.WithRequestWithResult<CreateUserClaimsCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;

    public CreateUserClimasHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/CreateUserClaims")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/CreateUserClaims",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateUserClaimsCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.CreateUserClaims(command, cancellationToken);
        return responce.ToOperationResult();
    }
}