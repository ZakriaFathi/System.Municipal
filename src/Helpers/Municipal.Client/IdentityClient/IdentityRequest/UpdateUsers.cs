using System.Security.Claims;
using Municipal.Application.Legacy.Features.UserManagement.Permissions;
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUser;
using Municipal.Domin.Models;
using Municipal.Utils.Enums;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class UpdateUsers
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string FatherName { get; set; }
    public string LastName { get; set; }
    public string Nationality { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string NationalId { get; set; }
    public string PassportId { get; set; }
    public DateTime PassportExpirationDate { get; set; }
    public string placeOfIssue { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public UserType UserType { get; set; }

    public List<UserClaims> Claims { get; set; }

    public static UpdateUsers Prepare(UpdateUserRequest updateUserDto, List<GetPermissionsResponse> allPermissions)
    {
        var Claims = new List<Claim>();
        foreach (var t in allPermissions)
        {
            t.Permissions.ForEach(x =>
            {
                updateUserDto.Permissions.ForEach(y =>
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

        return new UpdateUsers()
        {
            UserId = updateUserDto.UserId.ToString(),
            PhoneNumber = updateUserDto.PhoneNumber,
            Email = updateUserDto.Email,
            Address = updateUserDto.Address,
            UserName = updateUserDto.UserName,
            UserType = updateUserDto.UserType,
            FirstName = updateUserDto.FirstName,
            FatherName = updateUserDto.FatherName,
            LastName = updateUserDto.LastName,
            Nationality = updateUserDto.Nationality,
            Country = updateUserDto.Country,
            DateOfBirth = updateUserDto.DateOfBirth,
            Gender = updateUserDto.Gender.ToString("D"),
            NationalId = updateUserDto.NationalId,
            PassportId = updateUserDto.PassportId,
            PassportExpirationDate = updateUserDto.PassportExpirationDate,
            placeOfIssue = updateUserDto.placeOfIssue,
            Claims = userClaims,
        };
    }
}