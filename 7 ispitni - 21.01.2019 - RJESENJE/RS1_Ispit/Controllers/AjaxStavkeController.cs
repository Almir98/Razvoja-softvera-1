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
    public class AjaxStavkeController : Controller
    {
        private MojContext _context;
        public AjaxStavkeController(MojContext db)
        {
            _context = db;
        }
        
        public IActionResult Index(int maturskiID)
        {
            MaturskiIspit ispit = _context.MaturskiIspit.Find(maturskiID);

            var model = new Stavke_IndexVM
            {
                maturskiID=maturskiID,
                _lista=_context.MaturskiDetalji.Where(e=>e.MaturskiIspitID==maturskiID).Select(e=> new Stavke_IndexVM.rows
                {
                    stavkaID=e.ID,
                    ucenik=e.OdjeljenjeStavka.Ucenik.ImePrezime,
                    prosjek=e.Prosjek,
                    pristupio=e.Pristupio,
                    rezultat=e.Rezultat
                }).ToList()
            };
            return View(model);
        }
        public IActionResult Prisutan(int stavkaID)
        {
            MaturskiDetalji ispit = _context.MaturskiDetalji.Find(stavkaID);

            if (ispit.Pristupio)
            {
                ispit.Pristupio = false;
            }
            else
            {
                ispit.Pristupio = true;
            }
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?maturskiID="+ispit.MaturskiIspitID);
        }

        public IActionResult Uredi(int stavkaID)
        {
            MaturskiDetalji ispit = _context.MaturskiDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();


            var model = new Stavke_UrediVM
            {
                stavkaID=stavkaID,
                ucenik=ispit.OdjeljenjeStavka.Ucenik.ImePrezime,
                bodovi=ispit.Rezultat
            };

            return View(model);
        }

        public IActionResult SnimiUredi(Stavke_UrediVM ulaz)
        {
            MaturskiDetalji ispit = _context.MaturskiDetalji.Find(ulaz.stavkaID);

            ispit.Rezultat = ulaz.bodovi;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?maturskiID"+ispit.MaturskiIspitID);
        }


        public IActionResult SnimiBodove(int stavkaID,int bodovi)
        {
            MaturskiDetalji ispit = _context.MaturskiDetalji.Find(stavkaID);

            ispit.Rezultat =bodovi;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?maturskiID" + ispit.MaturskiIspitID);
        }
    }
}