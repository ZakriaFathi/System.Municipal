namespace Municipal.Application.Legacy.Models.UserManagement;

public class ChangePassword
{
    public Guid UserId { get; set; }
    public string OldPassWord { get; set; }
    public string NewPassWord { get; set; }
    public string ConfirmNewPassWord { get; set; }
}