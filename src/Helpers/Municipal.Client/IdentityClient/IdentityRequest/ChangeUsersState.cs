using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.ChangeUserActivation;
using Municipal.Utils.Enums;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class ChangeUsersState
{
    public Guid UserId { get; set; }
    public ActivateState State { get; set; }

    public static ChangeUsersState Prepare(ChangeUserActivationRequest request)
        => new ChangeUsersState()
        {
            UserId = request.UserId,
            State = request.State
        };
}