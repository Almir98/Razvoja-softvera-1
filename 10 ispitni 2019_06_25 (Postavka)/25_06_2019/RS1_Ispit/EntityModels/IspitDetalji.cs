using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class IspitDetalji
    {
        public int ID { get; set; }

        public int IspitID { get; set; }
        public Ispit Ispit { get; set; }

        public int UpisGodineID { get; set; }
        public UpisGodine UpisGodine { get; set; }

        public bool Pristupio { get; set; }
        public int? Ocjena { get; set; }
    }
}
