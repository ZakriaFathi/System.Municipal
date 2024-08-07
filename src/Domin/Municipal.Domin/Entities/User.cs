using Municipal.Domin.Common;
using Municipal.Utils.Enums;


namespace Municipal.Domin.Entities
{
    public class User : AuditableEntity
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ActivateState ActivateState { get; set; } = ActivateState.InActive;
        public UserType UserType { get; set; }
        public int? UsersLimit { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public UserKyc UserKyc { get; set; }
    }
}
