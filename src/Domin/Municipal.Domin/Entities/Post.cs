using Municipal.Domin.Common;

namespace Municipal.Domin.Entities;

public class Post : AuditableEntity
{
    public string Description { get; set; }
    public string UserName { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
}