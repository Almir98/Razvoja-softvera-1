using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class CasController : Controller
    {
        private MojContext _context;
        public CasController(MojContext db)
        {
            _context = db;
        }


        public IActionResult Index()
        {
            var model = new Cas_IndexVM
            {
                _lista=_context.Nastavnik.Select(e=>new Cas_IndexVM.rows
                {
                    nastavnikID=e.Id,
                    nastavnik=e.Ime+" "+e.Prezime,
                    skola=e.Skola.Naziv
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Odaberi(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            var model = new Cas_OdaberiVM
            {
                nastavnikID=nastavnikID,
                nastavnik=n.Ime+" "+n.Prezime,

                _lista=_context.OdrzaniCas.Where(e=>e.NastavnikID==nastavnikID).Select(e=>new Cas_OdaberiVM.rows
                {
                    odrzaniID=e.ID,
                    datum=e.Datum,
                    skola=e.Odjeljenje.SkolskaGodina.Naziv+"  /  "+e.Odjeljenje.Oznaka ,
                    predmet=e.Predmet.Naziv,
                    _odsutni=_context.OdrzaniDetalji.Where(a=>a.OdrzaniCasID==e.ID && a.Prisutan==false).Select(a=>a.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Uredi(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Where(e => e.ID == odrzaniID).Include(e => e.Odjeljenje).Include(e => e.Predmet).FirstOrDefault();

            var model = new Cas_UrediVM
            {
                odrzaniID=odrzaniID,
                nastavnikID=cas.NastavnikID,
                datum=cas.Datum,
                napomena=cas.Napomena,
                skupa=cas.Odjeljenje.Oznaka+"  /  "+cas.Predmet.Naziv
            };
            return View(model);
        }

        public IActionResult SnimiUredi(Cas_UrediVM ulaz)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Where(e => e.ID == ulaz.odrzaniID).Include(e => e.Odjeljenje).Include(e => e.Predmet).FirstOrDefault();

            cas.Napomena = ulaz.napomena;
            _context.SaveChanges();

            return Redirect("/Cas/Uredi?odrzaniID="+cas.ID);
        }

        public IActionResult Izbrisi(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Where(e => e.ID == odrzaniID).FirstOrDefault();
            var id = cas.NastavnikID;
            _context.Remove(cas);
            _context.SaveChanges();

            return Redirect("/Cas/Odaberi?nastavnikID="+id);
        }

        public IActionResult Dodaj(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            var model = new Cas_DodajVM
            {
                nastavnikID = nastavnikID,
                nastavnik = n.Ime + " " + n.Prezime,
                datum = DateTime.Now,

                _lista = _context.Odjeljenje.Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text=e.Oznaka+"  /  "+ _context.PredajePredmet.Where(a=>a.OdjeljenjeID==e.Id).Select(a=>a.Predmet.Naziv).FirstOrDefault()

                }).ToList()
            };
            return View(model);
        }


        public IActionResult Snimi(Cas_DodajVM ulaz)
        {
            Odjeljenje odjeljenje = _context.Odjeljenje.Where(e => e.Id == ulaz.odjeljenjeID).FirstOrDefault();
            PredajePredmet predaje = _context.PredajePredmet.Where(e => e.OdjeljenjeID == odjeljenje.Id).Include(e => e.Predmet).FirstOrDefault();

            OdrzaniCas novi = new OdrzaniCas
            {
                NastavnikID=ulaz.nastavnikID,
                Datum=ulaz.datum,
                OdjeljenjeID=ulaz.odjeljenjeID,
                PredmetID=predaje.PredmetID
            };
            _context.Add(novi);
            _context.SaveChanges();

            List<OdjeljenjeStavka> lista = _context.OdjeljenjeStavka.Where(e => e.OdjeljenjeId == novi.OdjeljenjeID).ToList();

            foreach (var i in lista)
            {
                OdrzaniDetalji od = new OdrzaniDetalji
                {
                    OdrzaniCasID=novi.ID,
                    OdjeljenjeStavkaID=i.Id,
                    Ocjena=5,
                    Prisutan=false,
                    Opravdano=true
                };
                _context.Add(od);
                _context.SaveChanges();
            }
            return Redirect("/Cas/Odaberi?nastavnikID=" + ulaz.nastavnikID);
        }
    }
}