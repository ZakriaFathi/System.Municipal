using Municipal.Domin.Entities;

namespace Municipal.Application.Legacy.Features.News.Posts.Queries;

public class GetPostsResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public List<CommentResponse> Comments { get; set; }
    public List<LikeResponse> Likes { get; set; }
}

public class CommentResponse
{
    public string Description { get; set; }
    public string UserName { get; set; }
}
public class LikeResponse
{
    public string UserName { get; set; }
}