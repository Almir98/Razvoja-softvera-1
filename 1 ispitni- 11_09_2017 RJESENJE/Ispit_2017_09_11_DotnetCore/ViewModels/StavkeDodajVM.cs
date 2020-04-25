using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class StavkeDodajVM
    {
        public int odjeljenjeID { get; set; }
        public int ucenikID { get; set; }
        public List<SelectListItem> ucenici { get; set; }

        public int broj { get; set; }
    }
}
