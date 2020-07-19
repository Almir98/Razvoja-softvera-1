using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Ispit
    {
        public int ID { get; set; }

        public int AngazovanID { get; set; }
        public Angazovan Angazovan { get; set; }

        public DateTime Datum { get; set; }
        public string Napomena { get; set; }
        public bool  Zakljucano { get; set; }
    }
}
