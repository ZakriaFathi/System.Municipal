using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Models.IdentityModel;

public class ChangeIdentityActivation
{
    public string UserId { get; set; }
    public ActivateState State { get; set; }
}