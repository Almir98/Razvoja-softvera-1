using RS1.Ispit.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Stavke_IndexVM
    {
        public int uputnicaID { get; set; }

        public List<rows> _lista { get; set; }

        public class rows
        {
            public int stavkaID { get; set; }
            public string pretraga { get; set; }
            public double? izmjereno { get; set; }
            public string JMJ { get; set; }
            public VrstaVrijednosti vrsta { get; set; }
        }
    }
}
