using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using test.ViewModels;

namespace test.Controllers
{
    public class StavkeController : Controller
    {
        private MojContext _context;
        public StavkeController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index(int maturskiID)
        {
            var model = new Stavke_IndexVM
            {
                maturskiID=maturskiID,
                
                _lista=_context.MaturskiIspitStavka.Where(e=>e.MaturskiIspitID==maturskiID).Select(e=>new Stavke_IndexVM.rows
                {
                    stavkaID=e.ID,
                    ucenik=e.UpisUOdjeljenje.Ucenik.ImePrezime,
                    uspjeh=e.UpisUOdjeljenje.OpciUspjeh,
                    bodovi=e.Bodovi ??0,
                    oslobodjen=e.Oslobodjen
                }).ToList()
            };
            return PartialView("Index",model);
        }

        public IActionResult SnimiBodove(int bodovi,int stavkaID)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Find(stavkaID);

            mis.Bodovi = bodovi;
            _context.SaveChanges();
            return Redirect("/Stavke/Index?maturskiID="+mis.MaturskiIspitID);
        }

        public IActionResult Uredi(int stavkaID)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Where(e => e.ID == stavkaID).Include(e => e.UpisUOdjeljenje.Ucenik).FirstOrDefault();

            var model =new Stavke_UrediVM{

                stavkaID=mis.ID,
                bodovi=mis.Bodovi??0,
                ucenik=mis.UpisUOdjeljenje.Ucenik.ImePrezime
            };

            return View(model);
        }

        public IActionResult Snimi(int bodovi, int stavkaID)
        {
            MaturskiIspitStavka mis = _context.MaturskiIspitStavka.Find(stavkaID);

            mis.Bodovi = bodovi;
            _context.SaveChanges();
            return Redirect("/Stavke/Index?maturskiID=" + mis.MaturskiIspitID);
        }






















    }
}