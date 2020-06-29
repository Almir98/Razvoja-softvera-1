using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_PrakticniDioIspita_2017_01_24.Models
{
    public class UpisUOdjeljenje
    {
        public int Id { get; set; }
        public int BrojUDnevniku { get; set; }
        public int OdjeljenjeID { get; set; }
        public Odjeljenje Odjeljenje { get; set; }
        public int UcenikID { get; set; }
        public Ucenik Ucenik { get; set; }
    }
}
