namespace Municipal.Application.Legacy.Features.News.Comments.Commands.UpdateComment;

public class UpdateCommentRequest
{
    public string Description { get; set; }
    public Guid CommentId { get; set; }
}