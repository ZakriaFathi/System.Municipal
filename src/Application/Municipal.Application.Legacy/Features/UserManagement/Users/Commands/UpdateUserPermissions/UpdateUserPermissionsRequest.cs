namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserPermissions;

public class UpdateUserPermissionsRequest 
{
    public Guid UserId { get; set; }
    public List<string> Permissions { get; set; } = new();
}