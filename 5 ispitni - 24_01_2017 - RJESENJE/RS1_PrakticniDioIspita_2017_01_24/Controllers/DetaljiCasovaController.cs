using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class DetaljiCasovaController : Controller
    {
        public MojContext _context;
        public DetaljiCasovaController(MojContext baza)
        {
            _context = baza;
        }


        public IActionResult Index(int odrzaniCasID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(odrzaniCasID);

            var model = new DetaljiCasova_IndexVM
            {
                odrzaniCasID=cas.Id,

                _detalji=_context.OdrzaniCasDetalj.Where(s=>s.odrzaniCasID==odrzaniCasID).Select(x=>new DetaljiCasova_IndexVM.Rows
                {
                    detaljCasaID=x.Id,
                    ucenikImePrezime=_context.UpisUOdjeljenje.Where(s=>s.Id==x.UpisUOdjeljenjeID).Select(e=>e.Ucenik.Ime).FirstOrDefault(),
                    ocjena=x.Ocjena,
                    prisutan=x.OpravdanoOdsutan,
                    odsutan=x.OpravdanoOdsutan

                }).ToList()
            };
            return PartialView("Index",model);
        }

        public IActionResult Izbrisi(int ID)
        {
            OdrzaniCasDetalj detalj = _context.OdrzaniCasDetalj.Find(ID);
            
            int stari = detalj.odrzaniCasID;

            _context.Remove(detalj);
            _context.SaveChanges();

            return Redirect("/DetaljiCasova/Index?odrzaniCasID=" + stari);
        }

        public IActionResult UrediPrisutan(int ID)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(ID);

            var model = new DetaljiCasova_UrediPrisutanVM
            {
                detaljCasaID=ocd.Id,
                ucenikImePrezime=_context.UpisUOdjeljenje.Where(s=>s.Id==ocd.UpisUOdjeljenjeID).Select(a=>a.Ucenik.Ime).FirstOrDefault(),
                ocjena=ocd.Ocjena
            };
            return PartialView("UrediPrisutan", model);
        }

        public IActionResult SnimiPrisutan(DetaljiCasova_UrediPrisutanVM ulaz)
        {

            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(ulaz.detaljCasaID);
            OdrzaniCas cas = _context.OdrzaniCas.Where(s => s.Id == ocd.odrzaniCasID).FirstOrDefault();

            ocd.Ocjena = ulaz.ocjena;
            _context.SaveChanges();
            return Redirect("/DetaljiCasova/Index?odrzaniCasID=" + cas.Id);
        }

        public IActionResult UrediOdsutan(int ID)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(ID);

            var model = new DetaljiCasova_UrediOdsutanVM
            {
                detaljCasID = ocd.Id,
                ucenikImePrezime = _context.UpisUOdjeljenje.Where(s => s.Id == ocd.UpisUOdjeljenjeID).Select(a => a.Ucenik.Ime).FirstOrDefault(),
                odabrano = 1        // nastimano da je 1 opravdano, 0 neopravdano u snimanju cemo vidjet selekciju
            };

            return PartialView("UrediOdsutan",model);
        }

        public IActionResult SnimiOdsutan(DetaljiCasova_UrediOdsutanVM ulaz)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(ulaz.detaljCasID);

            OdrzaniCas cas = _context.OdrzaniCas.Where(s => s.Id == ocd.odrzaniCasID).FirstOrDefault();

            if (ulaz.odabrano == 1)
            {
                ocd.OpravdanoOdsutan = true;
            }
            else
            {
                ocd.OpravdanoOdsutan = false;
            }
            _context.SaveChanges();
            return Redirect("/DetaljiCasova/Index?odrzaniCasID="+cas.Id);
        }

        public IActionResult Prisustvo(int ID)
        {
            OdrzaniCasDetalj ocd = _context.OdrzaniCasDetalj.Find(ID);

            OdrzaniCas cas = _context.OdrzaniCas.Where(s => s.Id == ocd.odrzaniCasID).FirstOrDefault();

            if (ocd.Odsutan == true)
            {
                ocd.Odsutan = true;
            }
            else
            {
                ocd.Odsutan = false;
            }
            _context.SaveChanges();
            return Redirect("/DetaljiCasova/Index?odrzaniCasID=" + cas.Id);
        }
    }
}