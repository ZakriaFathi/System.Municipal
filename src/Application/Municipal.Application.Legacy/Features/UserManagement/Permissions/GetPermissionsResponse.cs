namespace Municipal.Application.Legacy.Features.UserManagement.Permissions;

public class GetPermissionsResponse
{
    public string RoleName { get; set; }
    public List<Permissions> Permissions { get; set; }
}

public class Permissions
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
}