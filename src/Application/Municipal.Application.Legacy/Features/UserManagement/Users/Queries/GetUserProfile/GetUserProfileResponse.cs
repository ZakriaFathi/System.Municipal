namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;

public class GetUserProfileResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string UserType { get; set; }
}