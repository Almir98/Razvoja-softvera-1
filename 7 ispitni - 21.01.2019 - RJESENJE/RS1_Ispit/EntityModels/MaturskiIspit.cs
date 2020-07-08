using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspit
    {
        public int ID { get; set; }

        public int SkolaID { get; set; }
        public Skola Skola { get; set; }

        public int NastavnikID { get; set; }
        public Nastavnik Nastavnik { get; set; }

        public string SkolskaGodina { get; set; }

        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }

        public DateTime Datum { get; set; }
        public string Napomena { get; set; }
    }
}
