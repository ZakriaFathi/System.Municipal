using Municipal.Domin.Common;
using Municipal.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Domin.Entities
{
    public class Form : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FormType FormType { get; set; }
    }
}
