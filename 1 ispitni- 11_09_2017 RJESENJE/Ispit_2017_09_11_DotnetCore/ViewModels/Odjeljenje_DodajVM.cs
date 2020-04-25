using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class Odjeljenje_DodajVM
    {
        public string skolskaGodina { get; set; }
        public int razred { get; set; }
        public string oznaka { get; set; }

        public int nastavnikID { get; set; }
        public List<SelectListItem> nastavnici { get; set; }

        public int odjeljenjeID { get; set; }
        public List<SelectListItem> odjeljenja { get; set; }

    }
}
