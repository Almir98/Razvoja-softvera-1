using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class PopravniDetaljiController : Controller
    {
        public MojContext _context;
        public PopravniDetaljiController(MojContext db)
        {
            _context = db;
        }


        public IActionResult Index(int popravniID)
        {
            PopravniIspit ispit = _context.PopravniIspit.Find(popravniID);

            var model = new PopravniDetalji_IndexVM
            {
                popravniID=popravniID,

                _lista=_context.PopravniIspitDetalji.Where(a=>a.PopravniIspitID==popravniID).Select(e=>new PopravniDetalji_IndexVM.rows
                {
                    stavkaID=e.ID,
                    ucenik=_context.Ucenik.Where(a=>a.Id== e.OdjeljenjeStavka.UcenikId).Select(a=>a.ImePrezime).FirstOrDefault(),
                    brojDnevnik=e.OdjeljenjeStavka.BrojUDnevniku,
                    odjeljenje=_context.Odjeljenje.Where(a=>a.Id== e.OdjeljenjeStavka.OdjeljenjeId).Select(a=>a.Oznaka).FirstOrDefault(),
                    pristupio=e.pristupio,
                    rezultat=e.rezultat,
                }).ToList()
            };
            return PartialView("Index",model);
        }

        public IActionResult Uredi(int stavkaID)
        {
            PopravniIspitDetalji ispit = _context.PopravniIspitDetalji.Find(stavkaID);
            OdjeljenjeStavka odjeljenje = _context.OdjeljenjeStavka.Where(e => e.Id == ispit.OdjeljenjeStavkaID).FirstOrDefault();

            var model = new PopravniDetalji_UrediVM
            {
                stavkaID=ispit.ID,
                ucenik=_context.Ucenik.Where(e=>e.Id==odjeljenje.UcenikId).Select(e=>e.ImePrezime).FirstOrDefault(),
                bodovi=ispit.rezultat
            };

            return PartialView("Uredi", model);
        }

        public IActionResult Snimi(PopravniDetalji_UrediVM ulaz)
        {
            PopravniIspitDetalji ispit = _context.PopravniIspitDetalji.Find(ulaz.stavkaID);

            ispit.rezultat = ulaz.bodovi;
            _context.SaveChanges();

            return Redirect("/PopravniDetalji/Index?popravniID="+ispit.PopravniIspitID);
        }

        public IActionResult Promjeni(int stavkaID)
        {
            PopravniIspitDetalji ispit = _context.PopravniIspitDetalji.Find(stavkaID);

            if (ispit.pristupio)
            {
                ispit.pristupio = false;
            }
            else
            {
                ispit.pristupio = true;
            }
            _context.SaveChanges();
            
            return Redirect("/PopravniDetalji/Index?popravniID=" + ispit.PopravniIspitID);
        }






















    }
}