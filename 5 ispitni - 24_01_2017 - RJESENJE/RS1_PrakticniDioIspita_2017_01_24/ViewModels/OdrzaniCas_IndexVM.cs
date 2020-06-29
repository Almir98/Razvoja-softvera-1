using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class OdrzaniCas_IndexVM
    {
        public List<Rows> _listaNastavnika { get; set; }

        public class Rows
        {
            public int nastavnikID { get; set; }
            public string nastavnikImePrezime { get; set; }
        }
    }
}
