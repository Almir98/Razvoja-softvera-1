using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class OdrzaniCas_PrikazCasovaVM
    {
        public int nastavnikID { get; set; }

        public string nastavnikImePrezime { get; set; }

        public List<Rows> _listaPredmeta { get; set; }
        public class Rows
        {
            public int odrzaniCasID { get; set; }
            public DateTime datum { get; set; }
            public string  odjeljenje { get; set; }
            public string predmetNaziv { get; set; }
            public int brojPrisutnih { get; set; }
        }
    }
}
