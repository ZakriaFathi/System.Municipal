using Municipal.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Helpers
{
    public class PermissionAttribute : Attribute
    {
        public PermissionNames[] Permissions { get; }

        public PermissionAttribute(params PermissionNames[] permissions)
        {
            Permissions = permissions;
        }
    }
}
