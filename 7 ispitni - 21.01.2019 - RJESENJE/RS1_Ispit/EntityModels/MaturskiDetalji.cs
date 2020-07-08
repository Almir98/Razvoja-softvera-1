using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiDetalji
    {
        public int ID { get; set; }

        public int MaturskiIspitID { get; set; }
        public MaturskiIspit MaturskiIspit { get; set; }

        public int OdjeljenjeStavkaID { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }

        public float? Prosjek { get; set; }
        public bool Pristupio { get; set; }
        public int Rezultat { get; set; }
    }
}
