using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class Stavke_IndexVM
    {
        public int odjeljenjeID { get; set; }

        public List<Rows> _stavke { get; set; }

        public class Rows
        {
            public int odjeljenjaStavkeID { get; set; }
            public int broj_udnevniku { get; set; }
            public string ucenik_imePrezime { get; set; }
            public int? broj_zakljucnih { get; set; }
        }
    }
}
