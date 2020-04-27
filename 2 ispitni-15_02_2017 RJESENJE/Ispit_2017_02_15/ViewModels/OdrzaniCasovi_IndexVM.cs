using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdrzaniCasovi_IndexVM
    {
        public List<Rows> _lista { get; set; }

        public class Rows
        {
            public int nastavnikID { get; set; }
            public string imePrezime { get; set; }
        }
    }
}
