using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Enums
{
    [Flags]
    public enum PermissionNames
    {
        CreateUser = 1,
        ChangeUserActivation,
        ChangePassword,
        UploadSalary,
        DownloadSalary,
        UpdateUser,
        UpdateUserKyc,
        GetUsers,
        UpdateUserPermissions,
        ChangeReviewState,
        GetReviews,
        GetAllRoles,
        GetRolesByUserId,
        GetUserProfile,
        GetUserRolesAndPermissions,
    }
}
