using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using RS1_PrakticniDioIspita_2017_01_24.ViewModels;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class OdrzaniCasController : Controller
    {
        public MojContext _context;

        public OdrzaniCasController(MojContext db)
        {
            _context = db;
        }

        
        public IActionResult Index()
        {
            var model = new OdrzaniCas_IndexVM
            {
                _listaNastavnika = _context.Nastavnik.Select(s => new OdrzaniCas_IndexVM.Rows
                {
                    nastavnikID = s.Id,
                    nastavnikImePrezime = s.Ime
                }).ToList()
            };
            return View("Index",model);
        }

        public IActionResult PrikazCasova(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            Angazovan angazovan = _context.Angazovan.Where(s => s.NatavnikID == n.Id).FirstOrDefault();

            var model = new OdrzaniCas_PrikazCasovaVM
            {
                nastavnikID = n.Id,
                nastavnikImePrezime = n.Ime,

                _listaPredmeta = _context.OdrzaniCas.Where(s => s.AngazovanID == angazovan.Id).Select(x => new OdrzaniCas_PrikazCasovaVM.Rows
                {
                    odrzaniCasID=x.Id,
                    datum=x.datum,
                    odjeljenje=_context.Odjeljenje.Where(s=>s.NastavnikID==n.Id).Select(e=>e.Oznaka).FirstOrDefault(),
                    predmetNaziv=_context.Predmet.Where(s=>s.Id==angazovan.PredmetID).Select(e=>e.Naziv).Last(),
                    brojPrisutnih=_context.OdrzaniCasDetalj.Where(s=>s.Id==x.Id).Count(c=>c.Odsutan==false)
                }).ToList()
            };
            return View("PrikazCasova", model);
        }

        public IActionResult Dodaj(int nastavnikID)
        {
            Nastavnik n = _context.Nastavnik.Find(nastavnikID);

            List<Angazovan> _lista = _context.Angazovan.Include(p=>p.Predmet).Include(a=>a.Odjeljenje).Include(u=>u.Natavnik).Where(s => s.NatavnikID == nastavnikID).ToList();

            List < SelectListItem > nova= new List<SelectListItem>();

            nova.AddRange(_lista.Select(s => new SelectListItem
            {
                Value=s.Id.ToString(),
                Text =$"{s.Odjeljenje.Oznaka } / { s.Predmet.Naziv}"

            }).ToList());


            var model = new OdrzaniCas_DodajVM
            {
                nastavnikID=n.Id,
                nastavnikImePrezime=n.Ime,
                datum=DateTime.Now,
                lista=nova
            };

            return View("Dodaj", model);
        }


        public IActionResult Snimi(OdrzaniCas_DodajVM ulaz)
        {
            OdrzaniCas novi = new OdrzaniCas
            {
                datum = ulaz.datum,
                AngazovanID = ulaz.godinaPredmetID
            };
            _context.Add(novi);
            _context.SaveChanges();

            Angazovan angazovan = _context.Angazovan.Where(s => s.Id == novi.AngazovanID).FirstOrDefault();

            List<UpisUOdjeljenje> _lista = _context.UpisUOdjeljenje.Where(s => s.OdjeljenjeID == angazovan.OdjeljenjeID).ToList();

            foreach (var i in _lista)
            {
                OdrzaniCasDetalj ocd = new OdrzaniCasDetalj
                {
                    odrzaniCasID=novi.Id,
                    UpisUOdjeljenjeID=i.Id,
                    Odsutan=false
                };
                _context.Add(ocd);
            }
            _context.SaveChanges();

            return Redirect("/OdrzaniCas/PrikazCasova?nastavnikID=" + ulaz.nastavnikID);
        }

        public IActionResult Izbrisi(int casID)
        {
            OdrzaniCas stari = _context.OdrzaniCas.Find(casID);
            _context.Remove(stari);
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Edit(int odrzaniCasID)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(odrzaniCasID);
            Angazovan angazovan = _context.Angazovan.Where(x => x.Id == cas.AngazovanID).FirstOrDefault();
            Nastavnik n = _context.Nastavnik.Where(x => x.Id == angazovan.NatavnikID).FirstOrDefault();

            List<Angazovan> _lista2 = _context.Angazovan.Include(a=>a.Predmet).Include(e=>e.Odjeljenje).Include(u=>u.Natavnik).Where(s => s.NatavnikID == angazovan.NatavnikID).ToList();

            List<SelectListItem> nova = new List<SelectListItem>();

            nova.AddRange(_lista2.Select(x => new SelectListItem
            {
                Value=x.Id.ToString(),
                Text =$"{x.Odjeljenje.Oznaka},{x.Predmet.Naziv}"

            }).ToList());

            var model = new OdrzaniCas_EditVM
            {
                nastavnikID = n.Id,
                odrzaniCasID = odrzaniCasID,
                datum = cas.datum,
                lista=nova
            };

            return View("Edit", model);
        }

        public IActionResult SnimiEdit(OdrzaniCas_EditVM ulaz)
        {
            OdrzaniCas cas = _context.OdrzaniCas.Find(ulaz.odrzaniCasID);

            cas.datum = ulaz.datum;
            cas.AngazovanID = ulaz.godinaPredmetID;
            _context.SaveChanges();
            return Redirect("Index");
        }
    }
}   