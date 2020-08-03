using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Cas_OdabriVM
    {
        public int nastavnikID { get; set; }
        public string nastavnik { get; set; }
        public string skola { get; set; }


        public List<rows> _lista { get; set; }
        public class rows
        {
            public int odrzaniID { get; set; }
            public DateTime datum { get; set; }
            public string skolska { get; set; }
            public string odjeljenje { get; set; }
            public string predmet { get; set; }
            public List<string> _odsutni { get; set; }
        }
    }
}
