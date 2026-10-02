using System.Linq;
using System.Web.Mvc;
using kargo_takip1.Models;

namespace kargo_takip1.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
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
            return View(db.Kullanıcılar.ToList());
        }

        public ActionResult Ekle()
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kullanıcılar data)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            // GÜVENLİK KİLİDİ: Yeni eklenen her kullanıcı otomatik "D" (Misafir) rolüyle başlar.
            data.Rol = "D";

            db.Kullanıcılar.Add(data);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }

        public ActionResult Guncelle(int id)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");
            return View(db.Kullanıcılar.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Guncelle(Kullanıcılar model)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            var kargoHareket = db.Kullanıcılar.Find(model.Id);
            if (kargoHareket != null)
            {
                kargoHareket.kullaniciAdiSoyadi = model.kullaniciAdiSoyadi;
                kargoHareket.kullaniciMailAdresi = model.kullaniciMailAdresi;
                kargoHareket.Sifre = model.Sifre;
                kargoHareket.Rol = model.Rol;
                db.SaveChanges();
            }
            return RedirectToAction("Listele");
        }

        public ActionResult Sil(int id)
        {
            if (GetUserRole() != "A") return RedirectToAction("Listele");

            var kargo = db.Kullanıcılar.Where(x => x.Id == id).FirstOrDefault();
            if (kargo != null) { db.Kullanıcılar.Remove(kargo); db.SaveChanges(); }
            return RedirectToAction("Listele");
        }
    }
}