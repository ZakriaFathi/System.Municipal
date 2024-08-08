using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserProfile;
using Municipal.Utils.Enums;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class UpdateUsersProfile 
{
    public string UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public UserType UserType { get; set; }

    public static UpdateUsersProfile Prepare(UpdateUserProfileRequest request) 
        => new UpdateUsersProfile() 
        { 
            UserId = request.UserId.ToString(),
            Address = request.Address,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName,
            UserType = request.UserType,
        };

}