using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Nastava_DodajVM
    {
        public int nastavnikID { get; set; }
        public string nastavnik { get; set; }

        public DateTime datum { get; set; }

        public int skupaID { get; set; }
        public List<SelectListItem> _lista { get; set; }
        public string napomena { get; set; }
    }
}
