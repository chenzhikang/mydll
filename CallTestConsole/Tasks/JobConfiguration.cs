using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{

    public class JobConfiguration
    {
        public bool AllowLoop { get; set; }
        public int Interval { get; set; }
        public bool Enabled { get; set; }
        public bool StopOnException { get; set; }
        public string TypeName { get; set; }
    }
}
