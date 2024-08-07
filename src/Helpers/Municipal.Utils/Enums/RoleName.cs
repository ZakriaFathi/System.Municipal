using Municipal.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Enums
{
    public enum RoleName
    {
        [Permission(PermissionNames.DownloadSalary, PermissionNames.UploadSalary)]
        SalaryManagement = 1,

        [Permission(PermissionNames.ChangePassword, PermissionNames.UpdateUser, PermissionNames.UpdateUserKyc,
            PermissionNames.ChangeUserActivation, PermissionNames.CreateUser, PermissionNames.GetUsers,
            PermissionNames.UpdateUserKyc, PermissionNames.UpdateUserPermissions, PermissionNames.GetUserProfile)]
        UserManagement,

        [Permission(PermissionNames.GetUserRolesAndPermissions, PermissionNames.GetRolesByUserId,
            PermissionNames.GetAllRoles)]
        RolesManagement,

        [Permission(PermissionNames.GetReviews, PermissionNames.ChangeReviewState)]
        ReviewsManagement
    }
}
