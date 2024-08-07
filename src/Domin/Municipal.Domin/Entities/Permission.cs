using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Domin.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }

        public static Permission Create(int id, string name, int roleId)
        {
            return new Permission()
            {
                Id = id,
                Name = name,
                RoleId = roleId
            };
        }
    }
}
