using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.ViewModels
{
    public class Stavke_IndexVM
    {
        public int maturskiID { get; set; }
        public List<rows> _lista { get; set; }
        
        public class rows
        {
            public int stavkaID { get; set; }
            public string ucenik { get; set; }
            public int uspjeh { get; set; }
            public float? bodovi { get; set; }
            public bool oslobodjen { get; set; }
        }
    }
}
