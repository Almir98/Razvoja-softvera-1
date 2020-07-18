using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Ispit_DodajVM
    {
        public int odjeljenjeID { get; set; }
        public string odjeljenje { get; set; }

        public int predmetID { get; set; }
        public List<SelectListItem> _predmeti { get; set; }

        public DateTime datum { get; set; }

        public string skola { get; set; }
        public string skolska { get; set; }
    }
}
