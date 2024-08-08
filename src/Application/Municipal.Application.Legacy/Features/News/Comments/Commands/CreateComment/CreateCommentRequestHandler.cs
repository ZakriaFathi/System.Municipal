using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Comments.Commands.CreateComment;

public class CreateCommentRequestHandler : EndpointBase.WithRequestWithResult<CreateCommentRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public CreateCommentRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/CreateComment")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/CreateComment",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(CreateCommentRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.CreateComment(request, cancellationToken);
        return responce.ToOperationResult();    }
}