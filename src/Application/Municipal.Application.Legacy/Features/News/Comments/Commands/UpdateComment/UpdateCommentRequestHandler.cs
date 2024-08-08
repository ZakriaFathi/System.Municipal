using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.News.Comments.Commands.UpdateComment;

public class UpdateCommentRequestHandler : EndpointBase.WithRequestWithResult<UpdateCommentRequest, OperationResult<string>>
{
    private readonly INewsRepository _newsRepository;

    public UpdateCommentRequestHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpPost("api/News/UpdateComment")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "News/UpdateComment",
        Tags = new[] { "News" }
    )]
    public override async Task<OperationResult<string>> HandleAsync(UpdateCommentRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _newsRepository.UpdateComment(request, cancellationToken);
        return responce.ToOperationResult();    }
}