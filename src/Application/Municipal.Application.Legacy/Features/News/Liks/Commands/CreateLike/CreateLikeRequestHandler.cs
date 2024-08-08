using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Liks.Commands.CreateLike;

public class CreateLikeRequestHandler : EndpointBase.WithRequestWithResult<CreateLikeRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public CreateLikeRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/CreateLike")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/CreateLike",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(CreateLikeRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.CreateLike(request, cancellationToken);
        return responce.ToOperationResult();    }
}