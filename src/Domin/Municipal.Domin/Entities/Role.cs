using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Domin.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public static Role Create(int id, string name)
        {
            return new Role()
            {
                Id = id,
                Name = name
            };
        }
    }
}
