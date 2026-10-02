using System.Linq;
using System.Web.Mvc;
using kargo_takip1.Models;

namespace kargo_takip1.Controllers
{
    [Authorize]
    public class KargoFirmaController : Controller
    {
        adminEntities db = new adminEntities();

        private string GetUserRole()
        {
            string email = User.Identity.Name;
            var user = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == email);
            return user != null ? user.Rol : "D";
        }

        public ActionResult Listele()
        {
            ViewBag.Rol = GetUserRole();
            return View(db.KargoFirmaları.ToList());
        }

        public ActionResult Ekle()
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("Listele");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(KargoFirmaları data)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("Listele");

            db.KargoFirmaları.Add(data);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }

        public ActionResult Guncelle(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("Listele");

            return View(db.KargoFirmaları.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Guncelle(KargoFirmaları model)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("Listele");

            var kargoHareket = db.KargoFirmaları.Find(model.Id);
            if (kargoHareket != null)
            {
                kargoHareket.KargoAdi = model.KargoAdi;
                kargoHareket.KargoilgiliKisi = model.KargoilgiliKisi;
                kargoHareket.KargoTelNo = model.KargoTelNo;
                db.SaveChanges();
            }
            return RedirectToAction("Listele");
        }

        public ActionResult Sil(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("Listele");

            var kargo = db.KargoFirmaları.Where(x => x.Id == id).FirstOrDefault();
            if (kargo != null) { db.KargoFirmaları.Remove(kargo); db.SaveChanges(); }
            return RedirectToAction("Listele");
        }
    }
}