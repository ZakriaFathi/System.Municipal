

using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangePassword;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class ChangePasswords
{
    public string UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
 
    public static ChangePasswords Prepare(ChangePasswordRequest request)
    {
        return new ChangePasswords()
        {
            UserId = request.UserId.ToString(),
            OldPassword = request.OldPassWord,
            NewPassword = request.NewPassWord
        };
    }
}