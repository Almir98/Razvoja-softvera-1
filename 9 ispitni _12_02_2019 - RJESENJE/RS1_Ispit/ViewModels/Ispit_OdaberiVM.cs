using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Ispit_OdaberiVM
    {
        public int odjeljenjeID { get; set; }
        public string odjeljenje { get; set; }

        public List<rows> _lista { get; set; }

        public class rows
        {
            public int popravniID { get; set; }
            public DateTime datum { get; set; }
            public string predmet { get; set; }
            public int broj_Ucesnika { get; set; }
        }
    }
}
