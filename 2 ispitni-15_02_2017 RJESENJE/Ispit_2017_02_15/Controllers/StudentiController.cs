using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit_2017_02_15.Controllers
{
    public class StudentiController : Controller
    {
        public MojContext _context;

        public StudentiController(MojContext db)
        {
            _context = db;
        }

        public IActionResult Index(int odrzaniCasID)
        {
            OdrzaniCas cas = _context.OdrzaniCasovi.Find(odrzaniCasID);

            var model = new Studenti_IndexVM
            {
                odrzaniCasID = odrzaniCasID,

                _detalji = _context.OdrzaniCasDetalji.Where(s => s.OdrzaniCasId ==cas.Id).Select(x => new Studenti_IndexVM.Rows
                {
                    stavkaID = x.Id,
                    bodovi = x.BodoviNaCasu,
                    prisutan = x.Prisutan,
                    student=x.SlusaPredmet.UpisGodine.Student.Ime+" "+x.SlusaPredmet.UpisGodine.Student.Prezime
                }).ToList()
            };
            return PartialView("Index",model);
        }

        public IActionResult Izbrisi(int stavkaID)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(stavkaID);
            var nesto = ocd.OdrzaniCasId;

            _context.Remove(ocd);
            _context.SaveChanges();

            return Redirect("/Studenti/Index?odrzaniCasID=" + nesto);           // = FALIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII 
        }

        public IActionResult Detalji(int stavkaID)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(stavkaID);

            SlusaPredmet slusa = _context.SlusaPredmet.Where(s => s.Id == ocd.SlusaPredmetId).FirstOrDefault();
            UpisGodine upisGodine = _context.UpisGodine.Where(s => s.Id == slusa.UpisGodineId).FirstOrDefault();
            Student student = _context.Student.Where(s => s.Id == upisGodine.StudentId).FirstOrDefault();

            var model = new Studenti_Detalji_VM
            {
                stavkaID=ocd.Id,
                student=student.Ime+" "+student.Prezime,
                bodovi=ocd.BodoviNaCasu
            };
            return PartialView("Detalji", model);
        }

        public IActionResult Uredi(int stavkaID)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(stavkaID);

            SlusaPredmet slusa = _context.SlusaPredmet.Where(s => s.Id == ocd.SlusaPredmetId).FirstOrDefault();
            UpisGodine upis = _context.UpisGodine.Where(s => s.Id == slusa.UpisGodineId).FirstOrDefault();
            Student student = _context.Student.Where(s => s.Id == upis.StudentId).FirstOrDefault();

            var model = new Studenti_EditVM
            {
                stavkaID = ocd.Id,
                student=student.Ime+" "+student.Prezime,
                bodovi=ocd.BodoviNaCasu
            };

            return PartialView("Uredi", model);
        }

        public IActionResult Snimi(Studenti_EditVM ulaz)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(ulaz.stavkaID);

            ocd.BodoviNaCasu = ulaz.bodovi;
            _context.SaveChanges();

            return Redirect("/Studenti/Index?odrzaniCasID="+ocd.OdrzaniCasId);
        }

        public IActionResult Prisustvo(int stavkaID)
        {
            OdrzaniCasDetalji ocd = _context.OdrzaniCasDetalji.Find(stavkaID);

            if (ocd.Prisutan == true)
            {
                ocd.Prisutan = false;
            }
            else
            {
                ocd.Prisutan = true;
            }
            _context.SaveChanges();
            return Redirect("/Studenti/Index?odrzaniCasID=" + ocd.OdrzaniCasId);
        }
    }
}