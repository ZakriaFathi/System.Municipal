using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Commands.CreateForm;

public class CreateFormRequestHandler : EndpointBase.WithRequestWithResult<CreateFormRequest, OperationResult<string>>
{
    private readonly IFormRepository _formRepository;

    public CreateFormRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    [HttpPost("api/Forms/CreateForm")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/CreateForm",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] CreateFormRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.CreateForm(request, cancellationToken);
        return responce.ToOperationResult();
    }
}