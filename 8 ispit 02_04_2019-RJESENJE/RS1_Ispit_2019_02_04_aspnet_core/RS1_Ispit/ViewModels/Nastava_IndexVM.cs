using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Nastava_IndexVM
    {
        public List<rows> _lista { get; set; }

        public class rows
        {
            public int nastavnikID { get; set; }
            public string nastavnik { get; set; }
            public int broj_casova { get; set; }
        }
    }
}
