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
    public class OdjeljenjeStavkeController : Controller
    {
        public MojContext _context;
        public OdjeljenjeStavkeController(MojContext db)
        {
            _context = db;
        }


        public IActionResult Index(int odjeljenjeID)
        {
            var model = new Stavke_IndexVM
            {
                odjeljenjeID = odjeljenjeID,

                _stavke = _context.OdjeljenjeStavka.Where(a=>a.OdjeljenjeId==odjeljenjeID).Select(x => new Stavke_IndexVM.Rows
                {
                    odjeljenjaStavkeID=x.Id,
                    broj_udnevniku=x.BrojUDnevniku,
                    broj_zakljucnih=_context.DodjeljenPredmet.Where(e=>e.OdjeljenjeStavkaId==x.Id).Count(a=>a.ZakljucnoKrajGodine!=0),
                    ucenik_imePrezime=x.Ucenik.ImePrezime
                }).ToList()
            };
            return PartialView("Index",model);
        }

        public IActionResult Izbrisi(int stavkaID)
        {
            OdjeljenjeStavka os = _context.OdjeljenjeStavka.Find(stavkaID);
            var o = os.OdjeljenjeId;
            _context.Remove(os);
            _context.SaveChanges();
            
            return Redirect("/OdjeljenjeStavke/Index?odjeljenjeID="+o);
        }

        public IActionResult Dodaj(int odjeljenjeID)
        {
            var model = new StavkeDodajVM
            {
                odjeljenjeID = odjeljenjeID,
                broj = _context.OdjeljenjeStavka.Count(x => x.OdjeljenjeId == odjeljenjeID) + 1,

                ucenici = _context.Ucenik.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ImePrezime
                 }).ToList()

            };
            return PartialView("Dodaj", model);
        }

        public IActionResult Snimi(StavkeDodajVM ulaz)
        {
            OdjeljenjeStavka nova = new OdjeljenjeStavka
            {
                UcenikId = ulaz.ucenikID,
                OdjeljenjeId = ulaz.odjeljenjeID,
                BrojUDnevniku = ulaz.broj
            };
            _context.Add(nova);
            _context.SaveChanges();

            return PartialView("/OdjeljenjeStavke/Index?odjeljenjeID="+ulaz.odjeljenjeID);
        }

        public IActionResult Detalji(int stavkaID)
        {

            OdjeljenjeStavka s = _context.OdjeljenjeStavka.Find(stavkaID);
            Odjeljenje o = _context.Odjeljenje.Where(x => x.Id == s.OdjeljenjeId).FirstOrDefault();

            var model = new Stavke_DetaljiVM
            {
                odjeljenjeID=o.Id,
                odjeljenjeStavkeID=s.Id,
                ucenikImePrezime=_context.Ucenik.Where(x=>x.Id==s.UcenikId).Select(a=>a.ImePrezime).FirstOrDefault(),
                broj_Dnevniku=s.BrojUDnevniku
            };
            return PartialView("Detalji", model);
        }
    }
}