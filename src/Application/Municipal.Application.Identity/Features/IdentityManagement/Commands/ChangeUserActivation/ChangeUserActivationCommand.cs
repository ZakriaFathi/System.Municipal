using Municipal.Utils.Enums;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangeUserActivation;

public class ChangeUserActivationCommand 
{
    public string UserId { get; set; }
    public ActivateState State { get; set; }
}