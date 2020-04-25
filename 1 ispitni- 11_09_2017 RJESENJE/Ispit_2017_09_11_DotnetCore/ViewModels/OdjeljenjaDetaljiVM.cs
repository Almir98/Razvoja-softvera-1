using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class OdjeljenjaDetaljiVM
    {
        public int odjeljenjeID { get; set; }
        public string skolskagodina { get; set; }
        public int razred { get; set; }
        public string oznaka { get; set; }
        public string razrednik_Ime { get; set; }
        public int broj_predmeta { get; set; }
    }
}
