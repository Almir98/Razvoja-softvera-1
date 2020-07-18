using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class PopravniIspitDetalji
    {
        public int ID { get; set; }

        public int PopravniIspitID { get; set; }
        public PopravniIspit PopravniIspit { get; set; }

        public int OdjeljenjeStavkaID { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }

        public bool pristupio { get; set; }
        public int? rezultat { get; set; }
    }
}
