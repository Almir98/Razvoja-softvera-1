using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdrzaniCasovi_EditVM
    {
        public int casID { get; set; }
        public int nastavnikID { get; set; }
        public string nastavnik { get; set; }
        public string akgodina { get; set; }
        public DateTime datum { get; set; }
    }
}
