using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Posts.Commands.DeletePost;

public class DeletePostRequestHandler : EndpointBase.WithRequestWithResult<DeletePostRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public DeletePostRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/DeletePost")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/DeletePost",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(DeletePostRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.DeletePost(request, cancellationToken);
        return responce.ToOperationResult();    }
}