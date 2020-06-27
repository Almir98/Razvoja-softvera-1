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
    public class UputnicaController : Controller
    {
        private MojContext _context;
        public UputnicaController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            var model = new Uputnica_IndexVM
            {
                _lista=_context.Uputnica.Select(e=>new Uputnica_IndexVM.rows
                {
                    uputnicaID=e.Id,
                    uputio=e.DatumUputnice+"  |  "+ e.UputioLjekar.Ime,
                    pacijent=e.Pacijent.Ime,
                    vrsta=e.VrstaPretrage.Naziv,
                    datum=e.DatumRezultata
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Detalji(int uputnicaID)
        {
            Uputnica uputnica = _context.Uputnica.Where(e => e.Id == uputnicaID).Include(e => e.Pacijent).FirstOrDefault();

            var model = new Uputnica_DetaljiVM
            {
                uputnicaID=uputnicaID,
                datum=uputnica.DatumUputnice,
                datumr=uputnica.DatumRezultata,
                pacijent=uputnica.Pacijent.Ime
            };
            return View(model);
        }

        public IActionResult Dodaj()
        {
            var model = new Uputnica_DodajVM
            {
                _ljekari=_context.Ljekar.Select(e=>new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value=e.Id.ToString(),
                    Text=e.Ime
                }).ToList(),

                _vrste = _context.VrstaPretrage.Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Naziv
                }).ToList(),

                _pacijenti = _context.Pacijent.Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Ime
                }).ToList(),
                
                datum=DateTime.Now
            };
            return View(model);
        }

        public IActionResult Snimi(Uputnica_DodajVM ulaz)
        {
            Uputnica nova = new Uputnica
            {
                UputioLjekarId=ulaz.ljekarID,
                PacijentId=ulaz.pacijentID,
                VrstaPretrageId=ulaz.vrstaID,
                DatumUputnice=ulaz.datum,
                IsGotovNalaz=false,
                DatumRezultata=null
            };
            _context.Add(nova);
            _context.SaveChanges();

            List<LabPretraga> lista = _context.LabPretraga.Where(e => e.VrstaPretrageId == ulaz.vrstaID).ToList();

            foreach (var i in lista)
            {
                RezultatPretrage rp = new RezultatPretrage
                {
                    UputnicaId=nova.Id,
                    LabPretragaId=i.Id,
                };
                _context.Add(rp);
                _context.SaveChanges();
            }
            return Redirect("Index");
        }

        public IActionResult Zakljucaj(int uputnicaID)
        {
            Uputnica uputnica = _context.Uputnica.Where(e => e.Id == uputnicaID).FirstOrDefault();

            uputnica.IsGotovNalaz = true;
            uputnica.DatumRezultata = DateTime.Now;
            _context.SaveChanges();
            
            return Redirect("Index");
        }
    }
}