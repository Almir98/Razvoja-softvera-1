using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Uputnica_IndexVM
    {
        public List<rows> _lista { get; set; }
        public class rows
        {
            public int uputnicaID { get; set; }
            public string uputio { get; set; }
            public string pacijent { get; set; }
            public string vrsta { get; set; }
            public DateTime? datum { get; set; }
        }
    }
}
