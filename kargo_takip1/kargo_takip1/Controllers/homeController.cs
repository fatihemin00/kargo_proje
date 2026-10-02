using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using kargo_takip1.Models;

namespace kargo_takip1.Controllers
{
    [Authorize]
    public class homeController : Controller
    {
        adminEntities db = new adminEntities();

        // Rol Kontrol Metodu
        private string GetUserRole()
        {
            string email = User.Identity.Name;
            var user = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == email);
            return user != null ? user.Rol : "D";
        }

        public ActionResult Index()
        {
            var model = new KargoViewModel
            {
                GelenKargoList = db.GelenKargoTakip.ToList(),
                GidenKargoList = db.GidenKargoTakip.ToList()
            };
            return View(model);
        }

        // --- GİDEN KARGO İŞLEMLERİ ---
        public ActionResult gidenkargolarılistele()
        {
            ViewBag.Rol = GetUserRole();
            ViewBag.CurrentUserId = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == User.Identity.Name)?.Id;
            return View(db.GidenKargoTakip.ToList());
        }

        public ActionResult gidenKargoEkle()
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gidenkargolarılistele");

            ViewBag.Rol = rol;
            ViewBag.kargolar = (from a in db.KargoFirmaları.ToList() select new SelectListItem { Text = a.KargoAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.urunler = (from a in db.gidenUrunlerBilgi.ToList() select new SelectListItem { Text = a.GidenurunAdi + " -- " + a.GidenurunSeriNo, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult gidenKargoEkle(int kargoID, int gidenFirmaID, DateTime gonderilenTarih, string irsaliyeNo, string acıklama, int gonderenKisi, int[] urunID)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gidenkargolarılistele");

            if (urunID != null)
            {
                foreach (var id in urunID)
                {
                    db.GidenKargoTakip.Add(new GidenKargoTakip
                    {
                        kargoID = kargoID,
                        urunID = id,
                        gidenFirmaID = gidenFirmaID,
                        gonderilenTarih = gonderilenTarih,
                        irsaliyeNo = irsaliyeNo,
                        acıklama = acıklama,
                        gonderenKisi = gonderenKisi
                    });
                }
                db.SaveChanges();
            }
            return RedirectToAction("gidenkargolarılistele");
        }

        public ActionResult guncelle1(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gidenkargolarılistele");

            var guncelle1 = db.GidenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.kargolar = (from a in db.KargoFirmaları.ToList() select new SelectListItem { Text = a.KargoAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.urunler = (from a in db.gidenUrunlerBilgi.ToList() select new SelectListItem { Text = a.GidenurunAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            return View(guncelle1);
        }

        [HttpPost]
        public ActionResult guncelle1(GidenKargoTakip model)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gidenkargolarılistele");

            var kargoHareket = db.GidenKargoTakip.Find(model.Id);
            if (kargoHareket != null)
            {
                kargoHareket.kargoID = model.kargoID; kargoHareket.gidenFirmaID = model.gidenFirmaID;
                kargoHareket.urunID = model.urunID; kargoHareket.gonderilenTarih = model.gonderilenTarih;
                kargoHareket.irsaliyeNo = model.irsaliyeNo; kargoHareket.gonderenKisi = model.gonderenKisi;
                kargoHareket.acıklama = model.acıklama;
                db.SaveChanges();
            }
            return RedirectToAction("gidenkargolarılistele");
        }

        public ActionResult sil(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gidenkargolarılistele");

            var kargo = db.GidenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            if (kargo != null) { db.GidenKargoTakip.Remove(kargo); db.SaveChanges(); }
            return RedirectToAction("gidenkargolarılistele");
        }

        public ActionResult KargoKapağılistele(int id)
        {
            return View(db.GidenKargoTakip.Where(x => x.Id == id).ToList());
        }

        // --- GELEN KARGO İŞLEMLERİ ---
        public ActionResult gelenkargolarılistele()
        {
            ViewBag.Rol = GetUserRole();
            ViewBag.CurrentUserId = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == User.Identity.Name)?.Id;
            return View(db.GelenKargoTakip.ToList());
        }

        public ActionResult gelenKargoEkle()
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gelenkargolarılistele");

            ViewBag.Rol = rol;
            ViewBag.kargolar = (from a in db.KargoFirmaları.ToList() select new SelectListItem { Text = a.KargoAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.urunler = (from a in db.Urunler.ToList() select new SelectListItem { Text = a.urunAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar1 = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult GelenKargoEkle(int kargoID, int teslimAlanID, DateTime gelenTarih, string irsaliyeNo, string durum, int kontrolEdenID, int gonderenSirketID, int[] urunID)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gelenkargolarılistele");

            if (urunID != null)
            {
                foreach (var id in urunID)
                {
                    db.GelenKargoTakip.Add(new GelenKargoTakip
                    {
                        kargoID = kargoID,
                        urunID = id,
                        teslimAlanID = teslimAlanID,
                        gelenTarih = gelenTarih,
                        irsaliyeNo = irsaliyeNo,
                        durum = durum,
                        kontrolEdenID = kontrolEdenID,
                        gonderenSirketID = gonderenSirketID
                    });
                }
                db.SaveChanges();
            }
            return RedirectToAction("gelenkargolarılistele");
        }

        public ActionResult guncelle(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gelenkargolarılistele");

            var guncelle = db.GelenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.kargolar = (from a in db.KargoFirmaları.ToList() select new SelectListItem { Text = a.KargoAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.urunler = (from a in db.Urunler.ToList() select new SelectListItem { Text = a.urunAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            ViewBag.sirketler = (from a in db.Sirketler.ToList() select new SelectListItem { Text = a.sirketAdi, Value = a.Id.ToString() }).ToList();
            ViewBag.kullanıcılar1 = (from a in db.Kullanıcılar.ToList() select new SelectListItem { Text = a.kullaniciAdiSoyadi, Value = a.Id.ToString() }).ToList();
            return View(guncelle);
        }

        [HttpPost]
        public ActionResult guncelle(GelenKargoTakip model)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gelenkargolarılistele");

            var kargoHareket = db.GelenKargoTakip.Find(model.Id);
            if (kargoHareket != null)
            {
                kargoHareket.kargoID = model.kargoID; kargoHareket.urunID = model.urunID;
                kargoHareket.teslimAlanID = model.teslimAlanID; kargoHareket.gelenTarih = model.gelenTarih;
                kargoHareket.irsaliyeNo = model.irsaliyeNo; kargoHareket.durum = model.durum;
                kargoHareket.kontrolEdenID = model.kontrolEdenID; kargoHareket.gonderenSirketID = model.gonderenSirketID;
                db.SaveChanges();
            }
            return RedirectToAction("gelenkargolarılistele");
        }

        public ActionResult sil1(int id)
        {
            string rol = GetUserRole();
            if (rol != "A" && rol != "B") return RedirectToAction("gelenkargolarılistele");

            var kargo = db.GelenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            if (kargo != null) { db.GelenKargoTakip.Remove(kargo); db.SaveChanges(); }
            return RedirectToAction("gelenkargolarılistele");
        }
    }
}