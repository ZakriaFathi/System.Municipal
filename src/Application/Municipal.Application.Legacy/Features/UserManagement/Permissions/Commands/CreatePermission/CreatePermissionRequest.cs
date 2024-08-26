namespace Municipal.Application.Legacy.Features.UserManagement.Permissions.Commands.CreatePermission;

public class CreatePermissionRequest
{
    public string PermissionName{ get; set; }
    public int RoleId { get; set; }
}