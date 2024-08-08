namespace Municipal.Application.Identity.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommand 
{
    public string UserName { get; set; }

    public string NewPassword { get; set; }
    public string ConfiramNewPassword { get; set; }
}