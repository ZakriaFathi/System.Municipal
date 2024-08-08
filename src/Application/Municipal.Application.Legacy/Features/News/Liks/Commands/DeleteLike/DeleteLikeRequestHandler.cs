using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Liks.Commands.DeleteLike;

public class DeleteLikeRequestHandler : EndpointBase.WithRequestWithResult<DeleteLikeRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public DeleteLikeRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/DeleteLike")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/DeleteLike",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(DeleteLikeRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.DeleteLike(request, cancellationToken);
        return responce.ToOperationResult();    }
}