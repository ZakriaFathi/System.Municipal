using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.News.Comments.Commands.CreateComment;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Comments.Commands.DeleteComment;

public class DeleteCommentRequestHandler : EndpointBase.WithRequestWithResult<DeleteCommentRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public DeleteCommentRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/DeleteComment")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/DeleteComment",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(DeleteCommentRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.DeleteComment(request, cancellationToken);
        return responce.ToOperationResult();    }
}