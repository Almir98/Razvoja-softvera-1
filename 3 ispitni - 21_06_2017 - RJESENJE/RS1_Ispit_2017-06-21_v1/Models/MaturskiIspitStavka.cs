using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.Models
{
    public class MaturskiIspitStavka
    {
        public int ID { get; set; }
        public float? Bodovi { get; set; }
        public bool Oslobodjen { get; set; }

        public int MaturskiIspitID { get; set; }
        public MaturskiIspit MaturskiIspit { get; set; }

        public int UpisUOdjeljenjeID { get; set; }
        public UpisUOdjeljenje UpisUOdjeljenje { get; set; }
    }
}
