using Municipal.Utils.Enums;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand 
{
    public string UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public UserType UserType { get; set; }

}