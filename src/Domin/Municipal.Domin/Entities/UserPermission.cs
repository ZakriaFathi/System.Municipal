using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Domin.Entities
{
    public class UserPermission
    {
        public int PermissionId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}
