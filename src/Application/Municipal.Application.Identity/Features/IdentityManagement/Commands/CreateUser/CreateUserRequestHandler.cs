using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.CreateUser;

public class CreateUserRequestHandler : EndpointBase.WithRequestWithResult<CreateUserCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;

    public CreateUserRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/CreateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/CreateUser",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.CreateUser(command, cancellationToken);
        return responce.ToOperationResult();
    }
}