using Municipal.Domin.Common;
using Municipal.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Domin.Entities
{
    public class Order : AuditableEntity
    {
        public string UserName { get; set; }
        public OrderState OrderState { get; set; }

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
