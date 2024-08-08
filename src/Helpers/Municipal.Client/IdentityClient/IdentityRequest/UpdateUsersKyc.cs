
using Municipal.Application.Legacy.Features.UserManagement.Users.Commands.UpdateUserKyc;

namespace Municipal.Client.IdentityClient.IdentityRequest;

public class UpdateUsersKyc 
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

    public static UpdateUsersKyc Prepare(UpdateUserKycRequest request)
        => new UpdateUsersKyc() 
        {
            UserId = request.UserId.ToString(),
            FirstName = request.FirstName,
            FatherName = request.FatherName,
            LastName = request.LastName,
            Nationality = request.Nationality,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender.ToString("D"),
            NationalId = request.NationalId,
            PassportId = request.PassportId,
            PassportExpirationDate = request.PassportExpirationDate,
            placeOfIssue = request.placeOfIssue,
        };
}