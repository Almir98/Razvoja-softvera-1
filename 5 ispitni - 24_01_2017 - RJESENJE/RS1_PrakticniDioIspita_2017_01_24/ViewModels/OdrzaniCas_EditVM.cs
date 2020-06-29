using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class OdrzaniCas_EditVM
    {
        public int nastavnikID { get; set; }
        public int odrzaniCasID { get; set; }
        public DateTime datum { get; set; }
        public int godinaPredmetID { get; set; }
        public List<SelectListItem> lista { get; set; }
    }
}
