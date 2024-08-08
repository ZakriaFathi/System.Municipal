using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Commands.DeleteForm;

public class DeleteFormRequestHandler : EndpointBase.WithRequestWithResult<DeleteFormRequest, OperationResult<string>>
{
    private readonly IFormRepository _formRepository;

    public DeleteFormRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    [HttpDelete("api/Forms/DeleteForm")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/DeleteForm",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]DeleteFormRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.DeleteForm(request, cancellationToken);
        return responce.ToOperationResult();
    }
}