using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangePassword;

public class ChangePasswordRequestHandler : EndpointBase.WithRequestWithResult<ChangePasswordCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;
    public ChangePasswordRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }


    [HttpPost("api/IdentityManagement/ChangePassword")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/ChangePassword",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]ChangePasswordCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.ChangePassword(command, cancellationToken);
        return responce.ToOperationResult();
    }
}