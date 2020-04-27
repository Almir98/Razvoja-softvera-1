using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdrzaniCasovi_DetaljiCasovaVM
    {
        public int nastavnikID { get; set; }
        public string nastavnik { get; set; }

        public List<Rows> _casovi { get; set; }

        public class Rows
        {
            public int casID { get; set; }
            public DateTime datum { get; set; }
            public string akgodina { get; set; }
            public string predmet { get; set; }
            public int prisutnih { get; set; }
        }
    }
}
