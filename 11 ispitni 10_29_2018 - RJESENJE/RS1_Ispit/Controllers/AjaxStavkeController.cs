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

                _lista=_context.OdrzaniDetalji.Where(e=>e.OdrzaniCasID==odrzaniID).Select(e=>new Stavke_IndexVM.rows
                {
                    stavkaID=e.ID,
                    ucenik=e.OdjeljenjeStavka.Ucenik.ImePrezime,
                    ocjena=e.Ocjena,
                    opravdano=e.Opravdano,
                    prisutan=e.Prisutan
                }).ToList()
            };
            return PartialView(model);
        }

        public IActionResult Prisutan(int stavkaID)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Find(stavkaID);

            if (detalji.Prisutan)
                detalji.Prisutan = false;
            else
                 detalji.Prisutan = true;

            _context.SaveChanges();
            return Redirect("/AjaxStavke/Index?odrzaniID=" + detalji.OdrzaniCasID);
        }

        public IActionResult UrediPrisutan(int stavkaID)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            var model = new Stavke_UrediPrisutanVM
            {
                stavkaID=stavkaID,
                ucenik=detalji.OdjeljenjeStavka.Ucenik.ImePrezime,
                ocjena=detalji.Ocjena
            };
            return PartialView(model);
        }

        public IActionResult SnimiPrisutan(Stavke_UrediPrisutanVM ulaz)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Where(e => e.ID == ulaz.stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            detalji.Ocjena = ulaz.ocjena;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?odrzaniID=" + detalji.OdrzaniCasID);
        }

        public IActionResult UrediOdsutan(int stavkaID)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            var model = new Stavke_UrediOdsutanVM
            {
                stavkaID = stavkaID,
                ucenik = detalji.OdjeljenjeStavka.Ucenik.ImePrezime,
                napomena=detalji.Napomena,
                opravdano=detalji.Opravdano
            };
            return PartialView(model);
        }

        public IActionResult SnimiOdsutan(Stavke_UrediOdsutanVM ulaz)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Where(e => e.ID == ulaz.stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            detalji.Napomena = ulaz.napomena;
            detalji.Opravdano = ulaz.opravdano;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?odrzaniID=" + detalji.OdrzaniCasID);
        }

        public IActionResult SnimiOcjena(int stavkaID,int ocjena)
        {
            OdrzaniDetalji detalji = _context.OdrzaniDetalji.Where(e => e.ID == stavkaID).Include(e => e.OdjeljenjeStavka.Ucenik).FirstOrDefault();

            detalji.Ocjena = ocjena;
            _context.SaveChanges();
            
            return Redirect("/Cas/Uredi?odrzaniID=" + detalji.OdrzaniCasID);
        }
    }
}