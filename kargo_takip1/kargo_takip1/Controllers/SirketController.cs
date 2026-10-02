using System.Linq;
using System.Web.Mvc;
using kargo_takip1.Models;

namespace kargo_takip1.Controllers
{
    [Authorize]
    public class SirketController : Controller
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
            return View(db.Sirketler.ToList());
        }

        public ActionResult Ekle()
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Sirketler data)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            db.Sirketler.Add(data);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }

        public ActionResult Guncelle(int id)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");
            return View(db.Sirketler.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Guncelle(Sirketler model)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            var kargoHareket = db.Sirketler.Find(model.Id);
            if (kargoHareket != null)
            {
                kargoHareket.sirketAdi = model.sirketAdi; kargoHareket.sirketAdresi = model.sirketAdresi;
                kargoHareket.vergiNo = model.vergiNo; kargoHareket.vergiDairesi = model.vergiDairesi;
                kargoHareket.telefonNo = model.telefonNo; kargoHareket.ilgiliKisi = model.ilgiliKisi;
                db.SaveChanges();
            }
            return RedirectToAction("Listele");
        }

        public ActionResult Sil(int id)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            var kargo = db.Sirketler.Where(x => x.Id == id).FirstOrDefault();
            if (kargo != null) { db.Sirketler.Remove(kargo); db.SaveChanges(); }
            return RedirectToAction("Listele");
        }
    }
}