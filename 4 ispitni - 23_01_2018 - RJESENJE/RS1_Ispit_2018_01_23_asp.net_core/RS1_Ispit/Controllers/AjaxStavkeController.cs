using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1.Ispit.Web.Models;
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

        public IActionResult Index(int uputnicaID)
        {
            Uputnica uputnica = _context.Uputnica.Find(uputnicaID);

            var model = new Stavke_IndexVM
            {
                uputnicaID=uputnicaID,

                _lista=_context.RezultatPretrage.Where(e=>e.UputnicaId==uputnicaID).Select(e=>new Stavke_IndexVM.rows
                {
                    stavkaID=e.Id,
                    pretraga=e.LabPretraga.Naziv,
                    izmjereno=e.NumerickaVrijednost,
                    JMJ=e.LabPretraga.MjernaJedinica,

                    vrsta=e.LabPretraga.VrstaVr
                }).ToList()
            };
            return View(model);
        }


        public IActionResult UrediNumericka(int stavkaID)
        {
            RezultatPretrage rez = _context.RezultatPretrage.Where(e => e.Id == stavkaID).Include(e => e.LabPretraga).FirstOrDefault();

            var model = new Stavke_UrediNVM
            {
                stavkaID=stavkaID,
                pretraga=rez.LabPretraga.Naziv,
                vrijednost=rez.NumerickaVrijednost
            };
            return View(model);
        }

        public IActionResult SnimiNumericka(Stavke_UrediNVM ulaz)
        {
            RezultatPretrage rez = _context.RezultatPretrage.Where(e => e.Id == ulaz.stavkaID).Include(e => e.LabPretraga).FirstOrDefault();

            rez.NumerickaVrijednost = ulaz.vrijednost;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?uputnicaID="+rez.UputnicaId);
        }



        public IActionResult UrediModalitet(int stavkaID)
        {
            RezultatPretrage rez = _context.RezultatPretrage.Where(e => e.Id == stavkaID).Include(e => e.LabPretraga).FirstOrDefault();

            var model = new Stavke_UrediMVM
            {
                stavkaID = stavkaID,
                pretraga=rez.LabPretraga.Naziv,
                nekiID=1
            };
            return View(model);
        }

        public IActionResult SnimiModalitet(Stavke_UrediMVM ulaz)
        {
            RezultatPretrage rez = _context.RezultatPretrage.Where(e => e.Id == ulaz.stavkaID).Include(e => e.LabPretraga).FirstOrDefault();

            rez.ModalitetId = ulaz.nekiID;
            _context.SaveChanges();

            return Redirect("/AjaxStavke/Index?uputnicaID=" + rez.UputnicaId);
        }
    }
}