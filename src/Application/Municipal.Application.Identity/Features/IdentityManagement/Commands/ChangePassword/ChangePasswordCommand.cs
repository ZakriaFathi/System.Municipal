namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangePassword;

public class ChangePasswordCommand
{
    public string UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}