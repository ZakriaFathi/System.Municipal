using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Queries.GetFormByName;

public class GetFormByNameRequestHandler : EndpointBase.WithRequestWithResult<GetFormByNameRequest, OperationResult<GetFormResponse>>
{
    private readonly IFormRepository _formRepository;

    public GetFormByNameRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    [HttpGet("api/Forms/GetFormByName")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/GetFormByName",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<GetFormResponse>> HandleAsync([FromQuery]GetFormByNameRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.GetFormByName(request, cancellationToken);
        return responce.ToOperationResult();
    }
}