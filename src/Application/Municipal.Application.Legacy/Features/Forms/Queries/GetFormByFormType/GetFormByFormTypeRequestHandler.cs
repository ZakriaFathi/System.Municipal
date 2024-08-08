using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Forms.Queries.GetFormByFormType;

public class GetFormByFormTypeRequestHandler : EndpointBase.WithRequestWithResult<GetFormByFormTypeRequest, OperationResult<List<GetFormResponse>>>
{
    private readonly IFormRepository _formRepository;

    public GetFormByFormTypeRequestHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }
    [HttpGet("api/Forms/GetFormsByFormType")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Forms/GetFormsByFormType",
        Tags = new[] { "Forms" }
    )]
    public override async Task<OperationResult<List<GetFormResponse>>> HandleAsync([FromQuery]GetFormByFormTypeRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _formRepository.GetFormsByFormType(request, cancellationToken);
        return responce.ToOperationResult();
    }
}