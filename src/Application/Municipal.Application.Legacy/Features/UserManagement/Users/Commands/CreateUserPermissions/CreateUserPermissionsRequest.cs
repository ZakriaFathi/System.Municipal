namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUserPermissions;

public class CreateUserPermissionsRequest 
{
    public string UserId { get; set; }
    public List<string> Permissions { get; set; } = new();
}