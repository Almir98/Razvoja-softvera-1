﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class OdrzaniDetalji
    {
        public int ID { get; set; }

        public int OdrzaniCasID { get; set; }
        public OdrzaniCas OdrzaniCas { get; set; }

        public int OdjeljenjeStavkaID { get; set; }
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }

        public int Ocjena { get; set; }
        public bool Prisutan { get; set; }
        public bool Opravdano { get; set; }
        public string Napomena { get; set; }
    }
}
