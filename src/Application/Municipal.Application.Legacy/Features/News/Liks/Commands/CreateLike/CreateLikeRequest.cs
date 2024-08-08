namespace Municipal.Application.Legacy.Features.News.Liks.Commands.CreateLike;

public class CreateLikeRequest
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}