using Municipal.Domin.Common;

namespace Municipal.Domin.Entities;

public class Comment : AuditableEntity
{
    public string Description { get; set; }
    public string UserName { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}