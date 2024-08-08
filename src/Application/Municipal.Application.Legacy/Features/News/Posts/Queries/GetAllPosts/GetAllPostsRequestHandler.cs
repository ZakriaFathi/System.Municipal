using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Posts.Queries.GetAllPosts;

public class GetAllPostsRequestHandler : EndpointBase.WithRequestWithResult<GetAllPostsRequest, OperationResult<List<GetPostsResponse>>>
{
    private readonly INewsRepository _newsRepository;

    public GetAllPostsRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpGet("api/News/GetAllPosts")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/GetAllPosts",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<List<GetPostsResponse>>> HandleAsync([FromQuery]GetAllPostsRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.GetAllPosts(request, cancellationToken);
        return responce.ToOperationResult();    }
}