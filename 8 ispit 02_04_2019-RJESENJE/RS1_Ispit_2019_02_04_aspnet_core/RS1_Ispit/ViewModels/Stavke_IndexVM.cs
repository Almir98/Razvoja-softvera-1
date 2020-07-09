using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class Stavke_IndexVM
    {
        public int odrzaniID { get; set; }
        public bool zakljucaj { get; set; }
        public List<rows> _lista { get; set; }


        public class rows
        {
            public int stavkaID { get; set; }
            public String ucenik { get; set; }
            public int? ocjena { get; set; }
            public bool prisutan { get; set; }
            public bool opravdano { get; set; }
            public string napomena { get; set; }
        }








    }
}
