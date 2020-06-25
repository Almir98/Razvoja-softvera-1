using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class Ispit_IndexVM
    {
        public List<rows> _lista { get; set; }

        public class rows
        {
            public int maturskiID { get; set; }
            public DateTime datum { get; set; }
            public string odjeljenje { get; set; }
            public string ispitivac { get; set; }
            public float? prosjek { get; set; }
        }
    }
}
