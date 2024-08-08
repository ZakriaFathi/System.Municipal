namespace Municipal.Application.Legacy.Features.News.Posts.Commands.UpdatePost;

public class UpdatePostRequest
{
    public string Description { get; set; }
    public Guid PostId { get; set; }
}