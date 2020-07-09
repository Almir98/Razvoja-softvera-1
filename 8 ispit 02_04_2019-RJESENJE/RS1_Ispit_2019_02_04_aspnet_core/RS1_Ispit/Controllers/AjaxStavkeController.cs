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


        public IActionResult Index(int odrzaniID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(odrzaniID);

            var model = new Stavke_IndexVM
            {
                odrzaniID=odrzaniID,
                zakljucaj=cas.Zakljucaj,

                _lista=_context.OdrzaniCasDetalji.Where(e=>e.OdrzaniCasID==odrzaniID).Select(e=>new Stavke_IndexVM.rows
                {
                    stavkaID=e.ID,
                    ucenik=e.OdjeljenjeStavka.Ucenik.ImePrezime,
                    ocjena=e.Ocjena,
                    prisutan=e.Prisutan,
                    opravdano=e.Opravdano,
                    napomena=e.Napomena
                }).ToList()
            };
            return PartialView(model);
        }

        public IActionResult UrediPrisutan(int stavkaID)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            var model = new Stavke_UrediPrisutanVM
            {
                stavkaID=stavkaID,
                ucenik=cas.OdjeljenjeStavka.Ucenik.ImePrezime,
                ocjena=cas.Ocjena
            };
            return PartialView(model);
        }

        public IActionResult SnimiPrisutan(Stavke_UrediPrisutanVM ulaz)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Find(ulaz.stavkaID);

            cas.Ocjena = ulaz.ocjena;
            _context.SaveChanges();

            return Redirect("/Nastava/Detalji?odrzaniID="+cas.OdrzaniCasID);
        }

        public IActionResult Prisutan(int stavkaID)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Find(stavkaID);

            if (cas.Prisutan)
                cas.Prisutan = false;
            else
                cas.Prisutan = true;
            _context.SaveChanges();

            return Redirect("/Nastava/Detalji?odrzaniID=" + cas.OdrzaniCasID);
        }


        public IActionResult UrediOdsutan(int stavkaID)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            var model = new Stavke_UrediOdsutanVM
            {
                stavkaID = stavkaID,
                ucenik = cas.OdjeljenjeStavka.Ucenik.ImePrezime,
                opravdano = cas.Opravdano,
                napomena=cas.Napomena
            };
            return PartialView(model);
        }

        public IActionResult SnimiOdsutan(Stavke_UrediOdsutanVM ulaz)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Find(ulaz.stavkaID);

            cas.Napomena = ulaz.napomena;
            cas.Opravdano = ulaz.opravdano;
            _context.SaveChanges();

            return Redirect("/Nastava/Detalji?odrzaniID=" + cas.OdrzaniCasID);
        }

        public IActionResult SnimiBodove(int stavkaID,int ocjena)
        {
            OdrzaniCasDetalji cas = _context.OdrzaniCasDetalji.Find(stavkaID);

            cas.Ocjena = ocjena;
            _context.SaveChanges();

            return Redirect("/Nastava/Detalji?odrzaniID=" + cas.OdrzaniCasID);
        }
    }
}