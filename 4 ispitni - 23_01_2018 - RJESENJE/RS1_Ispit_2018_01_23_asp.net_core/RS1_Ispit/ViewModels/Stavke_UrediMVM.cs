using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Stavke_UrediMVM
    {
        public int stavkaID { get; set; }
        public string pretraga { get; set; }
        public int nekiID { get; set; }
    }
}
