using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Cas_UrediVM
    {
        public int odrzaniID { get; set; }
        public int nastavnikID { get; set; }
        public DateTime datum { get; set; }

        public string skupa { get; set; }
        public string napomena { get; set; }
    }
}
