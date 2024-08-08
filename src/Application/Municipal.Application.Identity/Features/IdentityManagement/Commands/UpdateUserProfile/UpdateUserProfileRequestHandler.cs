using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Identity.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserProfile;

public class UpdateUserProfileRequestHandler : EndpointBase.WithRequestWithResult<UpdateUserProfileCommand, OperationResult<string>>
{
    private readonly IIdentityManagementRepository _userManagementRepository;
    public UpdateUserProfileRequestHandler(IIdentityManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    [HttpPost("api/IdentityManagement/UpdateUserProfile")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "IdentityManagement/UpdateUserProfile",
        Tags = new[] { "IdentityManagement" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateUserProfileCommand command, CancellationToken cancellationToken = default)
    {
        var responce = await _userManagementRepository.UpdateUserProfile(command, cancellationToken);
        return responce.ToOperationResult();
    }
}