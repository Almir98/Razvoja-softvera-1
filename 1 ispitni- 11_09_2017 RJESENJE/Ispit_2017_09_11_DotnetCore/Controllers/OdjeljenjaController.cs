using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class OdjeljenjaController : Controller
    {
        public MojContext _context;
        public OdjeljenjaController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            var model = new Odjeljenje_IndexVM
            {
                _lista=_context.Odjeljenje.Select(x=>new Odjeljenje_IndexVM.Rows
                {
                    odjeljenjeID=x.Id,
                    skolskaGodina=x.SkolskaGodina,
                    razred=x.Razred,
                    oznaka=x.Oznaka,
                    razrednik_ImePrezime=x.Nastavnik.ImePrezime,
                    prebacen=x.IsPrebacenuViseOdjeljenje,
                    prosjek=_context.DodjeljenPredmet.Where(s=>s.OdjeljenjeStavka.OdjeljenjeId==x.Id).Average(e=>e.ZakljucnoKrajGodine) ??0,
                    najbolji_ucenik=false
                }).ToList()
            };
            return View("Index",model);
        }

        public IActionResult Izbrisi(int odjeljenjeID)
        {
            Odjeljenje staro = _context.Odjeljenje.Find(odjeljenjeID);
            _context.Remove(staro);
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Dodaj()
        {
            var model = new Odjeljenje_DodajVM
            {
                nastavnici = _context.Nastavnik.Select(x => new SelectListItem
                {
                    Value=x.NastavnikID.ToString(),
                    Text=x.ImePrezime
                }).ToList(),

                odjeljenja=_context.Odjeljenje.Where(s=>!s.IsPrebacenuViseOdjeljenje).Select(x=>new SelectListItem      // NIZA ODJELJENJA !!!!!!!!! 
                {
                    Value=x.Id.ToString(),
                    Text=x.SkolskaGodina+" "+x.Oznaka
                }).ToList()

            };
            return View("Dodaj", model);
        }

        public IActionResult Snimi(Odjeljenje_DodajVM ulaz)
        {
            Odjeljenje nize = _context.Odjeljenje.Find(ulaz.odjeljenjeID);

            Odjeljenje novo = new Odjeljenje        // NOVO ODJELJENJE
            {
                SkolskaGodina = ulaz.skolskaGodina,
                Razred = ulaz.razred,
                Oznaka = ulaz.oznaka,
                IsPrebacenuViseOdjeljenje = false,
                NastavnikID = ulaz.nastavnikID
            };
            _context.Add(novo);
            _context.SaveChanges();

            
            if(nize!=null)      // AKO JE IZABRAO NIZE ODJELJENJE
            {
                nize.IsPrebacenuViseOdjeljenje = true;

                List<OdjeljenjeStavka> _lista = _context.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == nize.Id).ToList();

                foreach (var i in _lista)
                {
                    OdjeljenjeStavka os = new OdjeljenjeStavka
                    {
                        OdjeljenjeId = novo.Id,
                        UcenikId = i.UcenikId,
                        BrojUDnevniku = 0
                    };
                    _context.Add(os);
                }
            }
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Detalji(int odjeljenjeID)
        {
            Odjeljenje postoji = _context.Odjeljenje.Where(x => x.Id == odjeljenjeID).FirstOrDefault();

            var model = new OdjeljenjaDetaljiVM
            {
                odjeljenjeID = odjeljenjeID,
                skolskagodina = _context.Odjeljenje.Where(x => x.Id == odjeljenjeID).Select(a => a.SkolskaGodina).FirstOrDefault(),
                razred = _context.Odjeljenje.Where(x => x.Id == odjeljenjeID).Select(a => a.Razred).FirstOrDefault(),
                oznaka = _context.Odjeljenje.Where(x => x.Id == odjeljenjeID).Select(a => a.Oznaka).FirstOrDefault(),
                razrednik_Ime = _context.Odjeljenje.Where(x => x.Id == odjeljenjeID).Select(a => a.Nastavnik.ImePrezime).FirstOrDefault(),
                broj_predmeta = _context.Predmet.Where(x => x.Razred == postoji.Razred).Count()
            };
            return View("Detalji", model);
        }
    }
}