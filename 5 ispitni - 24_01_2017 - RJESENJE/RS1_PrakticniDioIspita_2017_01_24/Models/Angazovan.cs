using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class Angazovan
    {
        public int Id { get; set; }
        public int? PredmetID { get; set; }
        public Predmet Predmet { get; set; }
        public int? NatavnikID { get; set; }
        public Nastavnik Natavnik { get; set; }
        public int? OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }

    }
}
