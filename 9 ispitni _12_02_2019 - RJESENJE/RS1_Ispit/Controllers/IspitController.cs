using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class IspitController : Controller
    {
        public MojContext _context;
        public IspitController(MojContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            var model = new Ispit_IndexVM
            {
                _lista=_context.Odjeljenje.Select(e=>new Ispit_IndexVM.rows
                {
                    OdjeljenjeID=e.Id,
                    odjeljenje=e.Oznaka,
                    skola=e.Skola.Naziv,
                    skolska=e.SkolskaGodina.Naziv
                }).ToList()
            };
            return View("Index",model);
        }
        public IActionResult Odaberi(int odjeljenjeID)
        {
            Odjeljenje odjeljenje = _context.Odjeljenje.Find(odjeljenjeID);
            
            var model = new Ispit_OdaberiVM
            {
                odjeljenjeID=odjeljenjeID,
                odjeljenje=odjeljenje.Oznaka,

                _lista = _context.PopravniIspit.Where(a=>a.OdjeljenjeID==odjeljenjeID).Select(e => new Ispit_OdaberiVM.rows
                {
                    popravniID=e.ID,
                    datum=e.Datum,
                    predmet=_context.Predmet.Where(a=>a.Id==e.PredmetID).Select(a=>a.Naziv).FirstOrDefault(),
                    broj_Ucesnika=_context.PopravniIspitDetalji.Count(a=>a.PopravniIspitID==e.ID),
                }).ToList()
            };
            return View("Odaberi", model);
        }

        public IActionResult Dodaj(int odjeljenjeID)
        {
            Odjeljenje odjeljenje = _context.Odjeljenje.Find(odjeljenjeID);

            var model = new Ispit_DodajVM
            {
                odjeljenjeID = odjeljenjeID,
                odjeljenje = odjeljenje.Oznaka,
                datum = DateTime.Now,

                skola = _context.Skola.Where(e => e.Id == odjeljenje.SkolaID).Select(e => e.Naziv).FirstOrDefault(),
                skolska=_context.SkolskaGodina.Where(e=>e.Id==odjeljenje.SkolskaGodinaID).Select(e=>e.Naziv).FirstOrDefault(),

                _predmeti=_context.Predmet.Select(e=> new SelectListItem {
                    
                    Value=e.Id.ToString(),
                    Text=e.Naziv

                }).ToList()
            };
            return View("Dodaj", model);
        }

        public IActionResult Snimi(Ispit_DodajVM ulaz)
        {
            Odjeljenje odjeljenje = _context.Odjeljenje.Find(ulaz.odjeljenjeID);

            PopravniIspit novi = new PopravniIspit
            {
                PredmetID=ulaz.predmetID,
                SkolaID=odjeljenje.SkolaID,
                SkolskaGodinaID=odjeljenje.SkolskaGodinaID,
                OdjeljenjeID=odjeljenje.Id,
                Datum=ulaz.datum
            };
            _context.Add(novi);
            _context.SaveChanges();

            List<OdjeljenjeStavka> _ucenici = _context.OdjeljenjeStavka.Where(e => e.OdjeljenjeId == odjeljenje.Id).ToList();
            // ovim mi imamo sve ucenike tog odjeljenja za koje se dodaje

            List<DodjeljenPredmet> negativci = _context.DodjeljenPredmet.Where(e => _ucenici.Any(a => a.Id == e.OdjeljenjeStavkaId) && e.PredmetId == novi.PredmetID && e.ZakljucnoKrajGodine == 1).ToList();
            // ovdje imamo sve predmete za novi i gdje je zakljucno kraj godine 1

            
            //List<DodjeljenPredmet> glavni = _context.DodjeljenPredmet.Where(e => negativci.Any(a => a.Id == e.Id) && (e.ZakljucnoKrajGodine == 1) > 3).ToList();

            foreach (var i in negativci)
            {
                PopravniIspitDetalji pid = new PopravniIspitDetalji
                {
                    PopravniIspitID=novi.ID,
                    OdjeljenjeStavkaID=i.OdjeljenjeStavkaId,
                    pristupio=false
                };
                _context.Add(pid);
            }
            _context.SaveChanges();
            
            return Redirect("/Ispit/Odaberi?odjeljenjeID="+ulaz.odjeljenjeID);
        }


        public IActionResult Uredi(int popravniID)
        {
            PopravniIspit ispit = _context.PopravniIspit.Find(popravniID);

            var model = new Ispit_UrediVM
            {
                popravniID=popravniID,
                predmet=_context.Predmet.Where(e=>e.Id==ispit.PredmetID).Select(e=>e.Naziv).FirstOrDefault(),
                datum=ispit.Datum,
                odjeljenje=_context.Odjeljenje.Where(e=>e.Id==ispit.OdjeljenjeID).Select(e=>e.Oznaka).FirstOrDefault(),
                skola=_context.Skola.Where(e=>e.Id==ispit.SkolaID).Select(e=>e.Naziv).FirstOrDefault(),
                skolska=_context.SkolskaGodina.Where(e=>e.Id==ispit.SkolskaGodinaID).Select(e=>e.Naziv).FirstOrDefault()
            };
            return View("Uredi", model);
        }
    }
}