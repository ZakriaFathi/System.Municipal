using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Queries.GetAllForms;

public class GetAllFormsRequestHandler : EndpointBase.WithRequestWithResult<GetAllFormsRequest, OperationResult<List<GetFormResponse>>>
{
    private readonly IFormRepository _formRepository;

    public GetAllFormsRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    [HttpGet("api/Forms/GetAllForms")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/GetAllForms",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<List<GetFormResponse>>> HandleAsync([FromQuery] GetAllFormsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.GetAll(request, cancellationToken);
        return responce.ToOperationResult();
    }
}