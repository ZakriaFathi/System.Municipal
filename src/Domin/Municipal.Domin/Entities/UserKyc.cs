

using Municipal.Utils.Enums;

namespace Municipal.Domin.Entities
{
    public class UserKyc
    {
        public int Id { get; set; }
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
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
