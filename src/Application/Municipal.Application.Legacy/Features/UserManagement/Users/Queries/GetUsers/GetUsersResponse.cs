using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUsers;

public class GetUsersResponse
{
    public Guid Id { get; set; }
    public UserType UserType { get; set; }
    public ActivateState ActivateState { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }

}