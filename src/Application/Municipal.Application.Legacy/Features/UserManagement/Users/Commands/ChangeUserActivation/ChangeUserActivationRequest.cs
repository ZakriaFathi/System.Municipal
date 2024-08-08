using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;

public class ChangeUserActivationRequest 
{ 
    public Guid UserId { get; set; }
    public ActivateState State { get; set; }
}