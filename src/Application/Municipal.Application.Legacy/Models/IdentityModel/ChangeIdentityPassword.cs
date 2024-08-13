namespace Municipal.Application.Legacy.Models.IdentityModel;

public class ChangeIdentityPassword
{
    public string UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}