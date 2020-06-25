using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;
using test.ViewModels;

namespace RS1_Ispit_2017_06_21_v1.Controllers
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
                _lista=_context.MaturskiIspit.Select(e=>new Ispit_IndexVM.rows
                {
                    maturskiID=e.ID,
                    datum=e.Datum,
                    odjeljenje=e.Odjeljenje.Naziv,
                    ispitivac=e.Nastavnik.ImePrezime,
                    prosjek=_context.MaturskiIspitStavka.Where(a=>a.MaturskiIspitID==e.ID).Average(a=>a.Bodovi)??0
                }).ToList()

            };
            return View(model);
        }

        public IActionResult Detalji(int maturskiID)
        {
            MaturskiIspit ispit = _context.MaturskiIspit.Where(e => e.ID == maturskiID).Include(e => e.Nastavnik).Include(e => e.Odjeljenje).FirstOrDefault();

            var model = new Ispit_DetaljiVM
            {
                maturskiID=ispit.ID,
                ispitivac=ispit.Nastavnik.ImePrezime,
                odjeljenje=ispit.Odjeljenje.Naziv,
                datum=ispit.Datum
            };
            return View(model);
        }
        public IActionResult Dodaj()
        {
            Nastavnik n = _context.Nastavnik.Find(1);
            var model = new Ispit_DodajVM
            {
                nastavnikID=1,
                nastavnik=n.ImePrezime,
                datum=DateTime.Now,
                _lista=_context.Odjeljenje.Select(e=> new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value=e.Id.ToString(),
                    Text=e.Naziv
                }).ToList()
            };
            return View(model);
        }


        public IActionResult Snimi(Ispit_DodajVM ulaz)
        {
            MaturskiIspit novi = new MaturskiIspit
            {
                Datum=ulaz.datum,
                NastavnikID=ulaz.nastavnikID,
                OdjeljenjeID=ulaz.odjeljenjeID
            };
            _context.Add(novi);
            _context.SaveChanges();

            List<UpisUOdjeljenje> _lista = _context.UpisUOdjeljenje.Where(e => e.OdjeljenjeId == novi.OdjeljenjeID && e.OpciUspjeh>1).ToList();

            foreach (var i in _lista)
            {
                if (i.OpciUspjeh == 5)
                {
                    MaturskiIspitStavka mis = new MaturskiIspitStavka
                    {
                        MaturskiIspitID=novi.ID,
                        UpisUOdjeljenjeID=i.Id,
                        Oslobodjen=true,
                        Bodovi=null
                    };
                    _context.Add(mis);
                    _context.SaveChanges();
                }
                else
                {
                    MaturskiIspitStavka mis = new MaturskiIspitStavka
                    {
                        MaturskiIspitID = novi.ID,
                        UpisUOdjeljenjeID = i.Id,
                        Oslobodjen = false,
                    };
                    _context.Add(mis);
                    _context.SaveChanges();
                }
            }
            return Redirect("Index");
        }
    }
}
