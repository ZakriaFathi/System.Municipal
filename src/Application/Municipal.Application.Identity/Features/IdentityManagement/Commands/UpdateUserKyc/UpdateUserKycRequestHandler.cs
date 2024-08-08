using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserKyc;

public class UpdateUserKycRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserKycCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;

    public UpdateUserKycRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/UpdateUserKyc")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserKyc",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserKycCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.UpdateUserKyc(command, cancellationToken);
        return responce.ToOperationResult();
    }
}