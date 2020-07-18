using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Ispit_IndexVM
    {
        public List<rows> _lista { get; set; }

        public class rows
        {
            public int OdjeljenjeID { get; set; }
            public string odjeljenje { get; set; }
            public string skolska { get; set; }
            public string skola { get; set; }
        }
    }
}
