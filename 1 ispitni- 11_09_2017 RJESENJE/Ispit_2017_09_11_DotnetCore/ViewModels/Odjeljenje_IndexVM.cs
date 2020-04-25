using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class Odjeljenje_IndexVM
    {
        public List<Rows> _lista { get; set; }

        public class Rows
        {
            public int odjeljenjeID { get; set; }
            public string skolskaGodina { get; set; }
            public int razred { get; set; }
            public string oznaka { get; set; }
            public string razrednik_ImePrezime { get; set; }
            public bool prebacen { get; set; }
            public double prosjek { get; set; }
            public bool najbolji_ucenik { get; set; }
        }

    }
}
