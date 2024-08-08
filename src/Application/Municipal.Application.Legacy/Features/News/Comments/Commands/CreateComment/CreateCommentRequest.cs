namespace Municipal.Application.Legacy.Features.News.Comments.Commands.CreateComment;

public class CreateCommentRequest
{
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}