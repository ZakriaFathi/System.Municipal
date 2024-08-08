using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;

public class CreateUserRequest 
{
    public string FirstName { get; set; }
    public string FatherName { get; set; }
    public string LastName { get; set; }
    public string Nationality { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string NationalId { get; set; }
    public string PassportId { get; set; }
    public DateTime PassportExpirationDate { get; set; }
    public string placeOfIssue { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    public List<string> Permissions { get; set; } = new();
}