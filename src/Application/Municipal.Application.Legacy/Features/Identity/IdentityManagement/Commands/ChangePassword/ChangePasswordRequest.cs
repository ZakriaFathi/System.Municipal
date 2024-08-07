namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangePassword;

public class ChangePasswordRequest
{
    public string UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}