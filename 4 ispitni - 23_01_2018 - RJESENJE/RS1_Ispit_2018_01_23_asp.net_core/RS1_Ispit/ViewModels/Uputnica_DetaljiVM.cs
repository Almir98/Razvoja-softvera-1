using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Uputnica_DetaljiVM
    {
        public int uputnicaID { get; set; }
        public DateTime datum { get; set; }
        public string pacijent { get; set; }
        public DateTime? datumr { get; set; }

    }
}
