using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Core.Abstracts;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Core.Features.RequestForm;

public class RequestFormCommandHandler : EndpointBase.WithRequestWithResult<RequestFormCommand, OperationResult<string>>
{
    private readonly IRequestRepository _requestRepository;

    public RequestFormCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    [HttpPost("api/Requests/RequestForm")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Requests/RequestForm",
        Tags = new[] { "Requests" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(RequestFormCommand request, CancellationToken cancellationToken = default)
    {
        var responce = await _requestRepository.RequestForm(request, cancellationToken);
        return responce.ToOperationResult();
    }
}