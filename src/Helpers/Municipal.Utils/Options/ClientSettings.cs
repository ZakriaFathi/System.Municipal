using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Options
{
    public class ClientSettings
    {
        public List<string> ClientId { get; set; }
        public string ClientSecrets { get; set; }
        public List<string> Scopes { get; set; }
        public string Url { get; set; }

    }
}
