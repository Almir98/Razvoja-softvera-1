using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Ispit_UrediVM
    {
        public int popravniID { get; set; }

        public string predmet { get; set; }
        public DateTime datum { get; set; }
        public string skola { get; set; }
        public string odjeljenje { get; set; }
        public string skolska { get; set; }

    }
}
