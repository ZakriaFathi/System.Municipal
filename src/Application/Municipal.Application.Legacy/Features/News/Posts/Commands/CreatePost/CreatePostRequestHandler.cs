using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Posts.Commands.CreatePost;

public class CreatePostRequestHandler : EndpointBase.WithRequestWithResult<CreatePostRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public CreatePostRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/CreatePost")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/CreatePost",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(CreatePostRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.CreatePost(request, cancellationToken);
        return responce.ToOperationResult();    }
}