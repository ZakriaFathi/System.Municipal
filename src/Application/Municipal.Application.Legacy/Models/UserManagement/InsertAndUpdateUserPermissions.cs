namespace Municipal.Application.Legacy.Models.UserManagement;

public class InsertAndUpdateUserPermissions
{
    public string UserId { get; set; }
    public List<string> Permissions { get; set; } = new();
}

