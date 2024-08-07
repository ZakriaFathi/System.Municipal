using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Identity.IdentityManagement.Commands.ChangeUserActivation;

public class ChangeUserActivationRequest 
{
    public string UserId { get; set; }
    public ActivateState State { get; set; }
}