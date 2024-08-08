namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;

public class ChangePasswordRequest
{
    public Guid UserId { get; set; }
    public string OldPassWord { get; set; }
    public string NewPassWord { get; set; }
    public string ConfirmNewPassWord { get; set; }
}