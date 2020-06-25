using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.Models
{
    public class MaturskiIspit
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }

        public int NastavnikID { get; set; }
        public Nastavnik Nastavnik { get; set; }

        public int OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
    }
}
