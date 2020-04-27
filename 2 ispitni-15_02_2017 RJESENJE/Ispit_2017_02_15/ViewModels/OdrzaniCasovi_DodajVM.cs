using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdrzaniCasovi_DodajVM
    {
        public int nastavnikID { get; set; }
        public string nastavnik { get; set; }
        public DateTime datum { get; set; }
        public int akgodinaID { get; set; }
        public List<SelectListItem> _lista { get; set; }
    }
}
