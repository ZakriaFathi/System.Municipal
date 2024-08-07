using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipal.Utils.Options
{
    public class ComponentConnectivityOptions
    {
        public LinkOptions[] LinkOptions { get; set; }
    }

    public class LinkOptions
    {
        public string LinkKey { get; set; }

        public string Link { get; set; }
    }
}
