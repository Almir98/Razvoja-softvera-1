using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.ViewModels
{
    public class DetaljiCasova_IndexVM
    {
        public int odrzaniCasID { get; set; }

        public List<Rows> _detalji { get; set; }

        public class Rows
        {
            public int detaljCasaID { get; set; }
            public string ucenikImePrezime { get; set; }
            public int? ocjena { get; set; }
            public bool? prisutan { get; set; }
            public bool? odsutan { get; set; }
        }
    }
}
