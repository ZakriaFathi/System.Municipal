using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Models.UserManagement;

public class ChangeUserActivation
{
    public Guid UserId { get; set; }
    public ActivateState State { get; set; }
}