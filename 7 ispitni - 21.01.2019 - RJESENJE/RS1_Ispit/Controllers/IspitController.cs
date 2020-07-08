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
    public class IspitController : Controller
    {
        private MojContext _context;
        public IspitController(MojContext db)
        {
            _context = db;
        }


        public IActionResult Index()
        {
            var model = new Ispit_IndexVM
            {
                _lista = _context.Nastavnik.Select(e => new Ispit_IndexVM.rows
                {
                    nastavnikID=e.Id,
                    nastavnik=e.Ime+" "+e.Prezime,
                    skola=_context.PredajePredmet.Where(a=>a.NastavnikID==e.Id).Select(a=>a.Odjeljenje.Skola.Naziv).FirstOrDefault() // ovo je ispravno !!
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Odaberi(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            var model = new Ispit_OdaberiVM
            {
                nastavnikID=nastavnikID,
                nastavnik=n.Ime+" "+n.Prezime,

                _lista=_context.MaturskiIspit.Where(e=>e.NastavnikID==nastavnikID).Select(e=>new Ispit_OdaberiVM.rows
                {
                    maturskiID=e.ID,
                    datum=e.Datum,
                    skola=e.Skola.Naziv,
                    predmet=e.Predmet.Naziv,

                    _odustni=_context.MaturskiDetalji.Where(a=>a.MaturskiIspitID==e.ID && a.Pristupio==false).Select(a=>a.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList()
            };
            return View(model);
        }


        public IActionResult Uredi(int maturskiID)
        {
            MaturskiIspit ispit = _context.MaturskiIspit.Where(e => e.ID == maturskiID).Include(e => e.Predmet).FirstOrDefault();

            var model = new Ispit_UrediVM
            {
                maturskiID=maturskiID,
                datum=ispit.Datum,
                predmet=ispit.Predmet.Naziv,
                napomena=ispit.Napomena
            };
            return View(model);
        }

        public IActionResult Snimi(Ispit_UrediVM ulaz)
        {
            MaturskiIspit ispit = _context.MaturskiIspit.Find(ulaz.maturskiID);

            ispit.Napomena = ulaz.napomena;
            _context.SaveChanges();

            return Redirect("/Ispit/Odaberi?nastavnikID="+ispit.NastavnikID);
        }


        public IActionResult Dodaj(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            Odjeljenje odjeljenje = _context.Odjeljenje.Where(e => e.RazrednikID == nastavnikID).FirstOrDefault();
            SkolskaGodina skolska = _context.SkolskaGodina.Where(e => e.Id == odjeljenje.SkolskaGodinaID).FirstOrDefault();

            var model = new Ispit_DodajVM
            {
                nastavnikID=nastavnikID,
                nastavnik=n.Ime+" "+n.Prezime,
                datum=DateTime.Now,
                skolska=skolska.Naziv,

                _predmeti=_context.Predmet.Select(e=> new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value=e.Id.ToString(),
                    Text=e.Naziv

                }).ToList(),

                _skole = _context.Skola.Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Naziv

                }).ToList()

            };
            return View(model);
        }


        public IActionResult SnimiDodaj(Ispit_DodajVM ulaz)
        {
            MaturskiIspit novi = new MaturskiIspit
            {
                SkolaID=ulaz.skolaID,
                NastavnikID=ulaz.nastavnikID,
                SkolskaGodina=ulaz.skolska,
                Datum=ulaz.datum,
                PredmetID=ulaz.predmetID
            };
            _context.Add(novi);
            _context.SaveChanges();

            List<OdjeljenjeStavka> lista = _context.OdjeljenjeStavka.Where(e => e.Odjeljenje.SkolaID == novi.SkolaID && e.Odjeljenje.Razred == 4).ToList();

            List<DodjeljenPredmet> pozitivni = _context.DodjeljenPredmet.Where(a => lista.Any(e => e.Id == a.OdjeljenjeStavkaId) && a.ZakljucnoKrajGodine > 1).ToList();

            List<MaturskiDetalji> nemamature = _context.MaturskiDetalji.Where(e => pozitivni.Any(a => a.OdjeljenjeStavkaId == e.OdjeljenjeStavkaID) && e.Rezultat < 55).ToList();

            foreach (var i in nemamature)
            {
                MaturskiDetalji md = new MaturskiDetalji
                {
                    MaturskiIspitID=novi.ID,
                    OdjeljenjeStavkaID=i.OdjeljenjeStavkaID,
                    Prosjek=i.Prosjek,
                    Pristupio=i.Pristupio,
                    Rezultat=i.Rezultat
                };
                _context.Add(md);
                _context.SaveChanges();
            }
            return Redirect("/Ispit/Odaberi?nastavnikID=" + ulaz.nastavnikID);
        }

    }
}