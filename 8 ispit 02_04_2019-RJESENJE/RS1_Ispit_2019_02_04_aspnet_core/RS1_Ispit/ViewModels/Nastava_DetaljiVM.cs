using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Nastava_DetaljiVM
    {
        public int odrzaniID { get; set; }
        public string nastavnik { get; set; }
        public string skupa { get; set; }
        public DateTime datum { get; set; }
        public string napomena { get; set; }
        public bool zakljucaj { get; set; }
    }
}
