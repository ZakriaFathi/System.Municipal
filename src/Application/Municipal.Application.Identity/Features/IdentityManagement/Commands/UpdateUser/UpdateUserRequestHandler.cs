using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUser;

public class UpdateUserRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;

    public UpdateUserRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }
    [HttpPost("api/IdentityManagement/UpdateUser")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUser",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.UpdateUser(command, cancellationToken);
        return responce.ToOperationResult();
    }
}