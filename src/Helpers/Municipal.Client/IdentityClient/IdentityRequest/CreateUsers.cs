using System.Security.Claims;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.CreateUser;
using Municipal.Domin.Models;
using Municipal.Utils.Enums;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class CreateUsers
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
    //public ActivateState ActivateState { get; set; }
    public List<UserClaims> Claims { get; set; }

    public static CreateUsers Prepare(CreateUserRequest createUserDto, List<GetPermissionsResponse> allPermissions)
    {
        var Claims = new List<Claim>();
        foreach (var t in allPermissions)
        {
            t.Permissions.ForEach(x =>
            {
                createUserDto.Permissions.ForEach(y =>
                {
                    if (y.ToString() == x.PermissionId.ToString())
                        Claims.Add(new Claim(x.PermissionName, t.RoleName));
                });
            });
        }
        var userClaims = Claims.GroupBy(x => x.Type).Select(claim => new UserClaims 
            { 
                type = claim.Key, 
                value = claim.Select(x => x.Value).ToList() 
            }).ToList();

        return new CreateUsers()
        {
            PhoneNumber = createUserDto.PhoneNumber,
            Email = createUserDto.Email,
            Address = createUserDto.Address,
            UserName = createUserDto.UserName,
            UserType = createUserDto.UserType,
            Password = createUserDto.Password,
            FirstName = createUserDto.FirstName,
            FatherName = createUserDto.FatherName,
            LastName = createUserDto.LastName,
            Nationality = createUserDto.Nationality,
            Country = createUserDto.Country,
            DateOfBirth = createUserDto.DateOfBirth,
            Gender = createUserDto.Gender,
            NationalId = createUserDto.NationalId,
            PassportId = createUserDto.PassportId,
            PassportExpirationDate = createUserDto.PassportExpirationDate,
            placeOfIssue = createUserDto.placeOfIssue,
            Claims = userClaims,
        };
    }
}