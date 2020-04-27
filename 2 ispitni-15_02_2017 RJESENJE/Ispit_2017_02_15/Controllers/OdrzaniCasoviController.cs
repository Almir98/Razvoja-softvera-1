using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ispit_2017_02_15.Controllers
{
    public class OdrzaniCasoviController : Controller
    {
        MojContext _context;
        public OdrzaniCasoviController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            var model = new OdrzaniCasovi_IndexVM
            {
                _lista = _context.Nastavnik.Select(x => new OdrzaniCasovi_IndexVM.Rows
                {
                    nastavnikID = x.Id,
                    imePrezime = x.Ime + " " + x.Prezime
                }).ToList()
            };
            return View("Index", model);
        }
        
        public IActionResult DetaljiCasova(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);


            var model = new OdrzaniCasovi_DetaljiCasovaVM
            {
                nastavnikID = n.Id,
                nastavnik = n.Ime + " " + n.Prezime,

                _casovi = _context.OdrzaniCasovi.Select(x => new OdrzaniCasovi_DetaljiCasovaVM.Rows
                {
                    casID = x.Id,
                    datum = x.Datum,
                    predmet=x.Angazovan.Predmet.Naziv,
                    akgodina=x.Angazovan.AkademskaGodina.Opis,  
                    prisutnih = _context.OdrzaniCasDetalji.Count(a => a.OdrzaniCasId == x.Id && a.Prisutan == true)         // VAZNO !!!!!!!!!!!

                }).ToList()
            };

            return View("DetaljiCasova", model);
        }

        public IActionResult Izbrisi(int casID)
        {
            OdrzaniCas cas = _context.OdrzaniCasovi.Find(casID);
            _context.Remove(cas);
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Edit(int casID)
        {
            OdrzaniCas cas = _context.OdrzaniCasovi.Find(casID);
            Angazovan angazovan = _context.Angazovan.Where(e => e.Id == cas.AngazovanId).FirstOrDefault();
            Nastavnik nastavnik = _context.Nastavnik.Where(s => s.Id == angazovan.NastavnikId).FirstOrDefault();

            AkademskaGodina akademska = _context.AkademskaGodina.Where(s => s.Id == angazovan.AkademskaGodinaId).FirstOrDefault();

            Predmet predmet = _context.Predmet.Where(r => r.Id == angazovan.PredmetId).FirstOrDefault();

            var model = new OdrzaniCasovi_EditVM
            {
                casID = cas.Id,
                nastavnikID = nastavnik.Id,
                nastavnik = nastavnik.Ime + " " + nastavnik.Prezime,
                datum = cas.Datum,
                akgodina=akademska.Opis+" / "+predmet.Naziv
            };

            return View("Edit", model);
        }

        public IActionResult Snimi(OdrzaniCasovi_EditVM ulaz)
        {
            OdrzaniCas cas = _context.OdrzaniCasovi.Find(ulaz.casID);

            cas.Datum = ulaz.datum;
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Dodaj(int nastavnikID)
        {
            Nastavnik nastavnik = _context.Nastavnik.Find(nastavnikID);

            List<Angazovan> angazovan = _context.Angazovan.Include(r => r.Predmet).Include(a => a.AkademskaGodina).Where(s => s.NastavnikId == nastavnik.Id).ToList();

            List < SelectListItem > nova= new List<SelectListItem>();

            nova.AddRange(angazovan.Select(x => new SelectListItem
            {
                Value=x.Id.ToString(),
                Text=x.AkademskaGodina.Opis+" / "+x.Predmet.Naziv
            }).ToList());

            var model = new OdrzaniCasovi_DodajVM
            {
                nastavnikID = nastavnik.Id,
                nastavnik = nastavnik.Ime + " " + nastavnik.Prezime,

                _lista =nova
            };
            return View("Dodaj", model);
        }

        public IActionResult SnimiNovi(OdrzaniCasovi_DodajVM ulaz)
        {
            OdrzaniCas novi = new OdrzaniCas
            {
                Datum = ulaz.datum,
                AngazovanId = ulaz.akgodinaID
            };

            _context.Add(novi);
            _context.SaveChanges();

            return Redirect("Index");
        }
    }
}