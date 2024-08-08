using Municipal.Domin.Common;

namespace Municipal.Domin.Entities;

public class Like : AuditableEntity
{
    public string UserName { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}