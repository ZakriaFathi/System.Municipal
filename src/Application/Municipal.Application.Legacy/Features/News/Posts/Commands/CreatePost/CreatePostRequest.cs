namespace Municipal.Application.Legacy.Features.News.Posts.Commands.CreatePost;

public class CreatePostRequest
{
    public string Description { get; set; }
    public Guid UserId { get; set; }
}