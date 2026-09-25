using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using kargo_takip1.Models;

namespace kargo_takip1.Controllers
{
    public class AccountController : Controller
    {
        adminEntities db = new adminEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(Kullanıcılar p)
        {
            var bilgiler = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == p.kullaniciMailAdresi && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullaniciMailAdresi, false);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Kullanıcı adı veya şifreniz hatalı" });
            }
        }

    }
}
