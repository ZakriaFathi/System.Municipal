using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Posts.Commands.UpdatePost;

public class UpdatePostRequestHandler : EndpointBase.WithRequestWithResult<UpdatePostRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public UpdatePostRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/UpdatePost")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/UpdatePost",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(UpdatePostRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.UpdatePost(request, cancellationToken);
        return responce.ToOperationResult();    }
}