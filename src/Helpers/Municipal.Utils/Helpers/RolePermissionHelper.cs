using Microsoft.OpenApi.Extensions;
using Municipal.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Helpers
{
    public static class RolePermissionHelper
    {
        public static List<RolesAndPermissions> GetRolePermissions()
        {
            var rolePermissions = new List<RolesAndPermissions>();

            var roleType = typeof(RoleName);
            var roleFields = roleType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var roleField in roleFields)
            {
                if (roleField.GetCustomAttribute(typeof(PermissionAttribute)) is not PermissionAttribute
                    permissionAttribute) continue;
                var role = (RoleName)roleField.GetValue(null)!;
                var permissions = permissionAttribute.Permissions.ToList();

                List<Permissions> permissionsList = permissions.Select(x => new Permissions
                {
                    PermissionId = (int)x,
                    PermissionName = x.GetDisplayName()
                }).ToList();

                rolePermissions.Add(new RolesAndPermissions
                {
                    RoleId = (int)role,
                    RoleName = role.ToString(),
                    Permissions = permissionsList
                });
            }
            return rolePermissions;
        }
    }

    public class RolesAndPermissions
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<Permissions> Permissions { get; set; }
    }

    public class Permissions
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}
