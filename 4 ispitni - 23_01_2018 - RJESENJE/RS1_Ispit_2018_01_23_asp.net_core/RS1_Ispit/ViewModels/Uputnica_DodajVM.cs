using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Uputnica_DodajVM
    {
        public int ljekarID { get; set; }
        public List<SelectListItem> _ljekari { get; set; }

        public int vrstaID { get; set; }
        public List<SelectListItem> _vrste { get; set; }

        public int pacijentID { get; set; }
        public List<SelectListItem> _pacijenti { get; set; }

        public DateTime datum { get; set; }
    }
}
