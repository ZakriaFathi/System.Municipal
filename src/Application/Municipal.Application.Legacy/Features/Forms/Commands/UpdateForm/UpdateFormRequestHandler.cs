using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Commands.UpdateForm;

public class UpdateFormRequestHandler : EndpointBase.WithRequestWithResult<UpdeteFormRequest, OperationResult<string>>
{
    private readonly IFormRepository _formRepository;

    public UpdateFormRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    [HttpPost("api/Forms/UpdateForm")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/UpdateForm",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdeteFormRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.UpdateForm(request, cancellationToken);
        return responce.ToOperationResult();
    }
}