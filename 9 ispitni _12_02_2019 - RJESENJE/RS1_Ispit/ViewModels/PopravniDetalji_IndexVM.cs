using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PopravniDetalji_IndexVM
    {
        public int popravniID { get; set; }

        public List<rows> _lista { get; set; }

        public class rows
        {
            public int stavkaID { get; set; }

            public string ucenik { get; set; }
            public string odjeljenje { get; set; }
            public int brojDnevnik { get; set; }
            public bool pristupio  { get; set; }
            public int? rezultat { get; set; }
        }
    }
}
