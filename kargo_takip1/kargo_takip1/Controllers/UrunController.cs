using System.Linq;
using System.Web.Mvc;
using kargo_takip1.Models;
using System.Collections.Generic;

namespace kargo_takip1.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        adminEntities db = new adminEntities();

        private string GetUserRole()
        {
            string email = User.Identity.Name;
            var user = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == email);
            return user != null ? user.Rol : "D";
        }

        // ---------------- 1. GELEN ÜRÜNLER ----------------
        public ActionResult GelenListele()
        {
            ViewBag.Rol = GetUserRole();
            return View(db.Urunler.ToList());
        }

        public ActionResult GelenEkle()
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GelenListele");
            return View();
        }

        [HttpPost]
        public ActionResult GelenEkle(Urunler data)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GelenListele");

            db.Urunler.Add(data);
            db.SaveChanges();
            return RedirectToAction("GelenListele");
        }

        public ActionResult GelenSil(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GelenListele");

            var urun = db.Urunler.Where(x => x.Id == id).FirstOrDefault();
            if (urun != null) { db.Urunler.Remove(urun); db.SaveChanges(); }
            return RedirectToAction("GelenListele");
        }

        // ---------------- 2. GİDEN ÜRÜNLER ----------------
        public ActionResult GidenListele()
        {
            ViewBag.Rol = GetUserRole();
            return View(db.gidenUrunlerBilgi.ToList());
        }

        public ActionResult GidenEkle()
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GidenListele");

            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult GidenEkle(gidenUrunlerBilgi data)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GidenListele");

            db.gidenUrunlerBilgi.Add(data);
            db.SaveChanges();
            return RedirectToAction("GidenListele");
        }

        public ActionResult GidenGuncelle(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GidenListele");

            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            return View(db.gidenUrunlerBilgi.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult GidenGuncelle(gidenUrunlerBilgi model)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GidenListele");

            var urun = db.gidenUrunlerBilgi.Find(model.Id);
            if (urun != null)
            {
                urun.GidenurunAdi = model.GidenurunAdi;
                urun.GidenurunSeriNo = model.GidenurunSeriNo;
                urun.gidenFirma = model.gidenFirma;
                db.SaveChanges();
            }
            return RedirectToAction("GidenListele");
        }

        public ActionResult GidenSil(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B" && rol != "C") return RedirectToAction("GidenListele");

            var urun = db.gidenUrunlerBilgi.Where(x => x.Id == id).FirstOrDefault();
            if (urun != null) { db.gidenUrunlerBilgi.Remove(urun); db.SaveChanges(); }
            return RedirectToAction("GidenListele");
        }
    }
}