namespace Municipal.Application.Legacy.Features.Identity.Auth.Commands.ResetPassword;

public class ResetPasswordRequest 
{
    public string UserName { get; set; }

    public string NewPassword { get; set; }
    public string ConfiramNewPassword { get; set; }
}