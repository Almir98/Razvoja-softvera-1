using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class Studenti_IndexVM
    {
        public int odrzaniCasID { get; set; }

        public List<Rows> _detalji { get; set; }

        public class Rows
        {
            public int stavkaID { get; set; }
            public string student { get; set; }
            public int bodovi { get; set; }
            public bool prisutan { get; set; }
        }
    }
}
