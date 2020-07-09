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
    public class NastavaController : Controller
    {
        private MojContext _context;
        public NastavaController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            var model = new Nastava_IndexVM
            {
                _lista=_context.Nastavnik.Select(e=>new Nastava_IndexVM.rows
                {
                    nastavnikID=e.Id,
                    nastavnik=e.Ime+" "+e.Prezime,
                    broj_casova=_context.OdrzaniCas.Count(a=>a.NastavnikID==e.Id)
                }).ToList()
            };
            return View(model);
        }
        public IActionResult Odaberi(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            var model = new Nastava_OdaberiVM
            {
                nastavnikID=nastavnikID,
                nastavnik=n.Ime+" "+n.Prezime,  

                _lista = _context.OdrzaniCas.Where(e=>e.NastavnikID==nastavnikID).Select(e => new Nastava_OdaberiVM.rows
                {
                    odrzaniID=e.ID,
                    datum=e.Datum,
                    skola=e.Skola.Naziv,
                    skodjeljenje=e.Odjeljenje.SkolskaGodina.Naziv+ "  /  "+ e.Odjeljenje.Oznaka,
                    predmet=e.Predmet.Naziv,
                    _odsutni=_context.OdrzaniCasDetalji.Where(a=>a.OdrzaniCasID==e.ID && a.Prisutan==false).Select(a=>a.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Detalji(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Where(e => e.ID == odrzaniID).Include(e => e.Skola).Include(e => e.Odjeljenje.SkolskaGodina).Include(e => e.Odjeljenje).Include(e => e.Predmet).Include(e=>e.Nastavnik).FirstOrDefault();

            var model = new Nastava_DetaljiVM
            {
                odrzaniID=odrzaniID,
                nastavnik=cas.Nastavnik.Ime+" "+cas.Nastavnik.Prezime,
                skupa=cas.Skola.Naziv+" / "+cas.Odjeljenje.Oznaka+" / "+cas.Predmet.Naziv,
                datum=cas.Datum,
                napomena=cas.Napomena,
                zakljucaj=false
            };
            return View(model);
        }

        public IActionResult Izbrisi(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(odrzaniID);
            var id = cas.NastavnikID;
            _context.Remove(cas);
            _context.SaveChanges();

            return Redirect("/Nastava/Odaberi?nastavnikID="+id);
        }
        public IActionResult Zakljucaj(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(odrzaniID);
            cas.Zakljucaj = true;
            _context.SaveChanges();

            return Redirect("/Nastava/Detalji?odrzaniID=" + odrzaniID);
        }

        public IActionResult Dodaj(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            var model = new Nastava_DodajVM
            {
                nastavnikID=nastavnikID,
                nastavnik=n.Ime+" "+n.Prezime,
                datum=DateTime.Now,

                _lista=_context.Odjeljenje.Select(e=>new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value=e.Id.ToString(),
                    Text=e.Skola.Naziv+" / "+e.Oznaka+" / "+ _context.PredajePredmet.Where(a=>a.OdjeljenjeID==e.Id).Select(a=>a.Predmet.Naziv).FirstOrDefault()
                }).ToList()

            };
            return View(model);
        }

        public IActionResult Snimi(Nastava_DodajVM ulaz)
        {
            Odjeljenje odjeljenje = _context.Odjeljenje.Find(ulaz.skupaID);

            Skola skola = _context.Skola.Where(e => e.Id == odjeljenje.SkolaID).FirstOrDefault();
            PredajePredmet predaje = _context.PredajePredmet.Where(e => e.OdjeljenjeID == ulaz.skupaID).FirstOrDefault();
            Predmet predmet = _context.Predmet.Where(e => e.Id == predaje.PredmetID).FirstOrDefault();

            OdrzaniCas novi = new OdrzaniCas
            {
                NastavnikID=ulaz.nastavnikID,
                Datum=ulaz.datum,
                Napomena=ulaz.napomena,
                OdjeljenjeID=ulaz.skupaID,
                SkolaID=skola.Id,
                PredmetID=predmet.Id,
                Zakljucaj=false
            };
            _context.Add(novi);
            _context.SaveChanges();

            List<OdjeljenjeStavka> lista = _context.OdjeljenjeStavka.Where(e => e.OdjeljenjeId == novi.OdjeljenjeID).ToList();

            foreach (var i in lista)
            {
                OdrzaniCasDetalji ocd = new OdrzaniCasDetalji
                {
                    OdrzaniCasID=novi.ID,
                    OdjeljenjeStavkaID=i.Id,
                    Prisutan=false,
                    Opravdano=false
                };
                _context.Add(ocd);
                _context.SaveChanges();
            }
            return Redirect("/Nastava/Odaberi?nastavnikID="+ulaz.nastavnikID);
        }
    }
}