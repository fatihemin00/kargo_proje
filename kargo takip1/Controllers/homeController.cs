using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kargo_takip1.Models;
using System.Security.Claims;
using System.Data.Entity;
namespace kargo_takip1.Controllers
{
    [Authorize]
    public class homeController : Controller
    {
        
        adminEntities db = new adminEntities();
        // GET: home
        
        public ActionResult Index()
        {
            var model = new KargoViewModel
            {
                GelenKargoList = db.GelenKargoTakip.ToList(),
                GidenKargoList = db.GidenKargoTakip.ToList()
            };

            return View(model);
        }
        private List<GelenKargoTakip> gelenKargo()
        {
            // Veritabanından veya başka bir kaynaktan Kargo verilerini al
            return new List<GelenKargoTakip>();
        }

        private List<GidenKargoTakip> gidenKargo()
        {
            // Veritabanından veya başka bir kaynaktan DiğerTablo verilerini al
            return new List<GidenKargoTakip>();
        }

        public ActionResult sil(int id)
        {
            var kargo = db.GidenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            db.GidenKargoTakip.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("gidenkargolarılistele");
        }
        public ActionResult sil1(int id)
        {
            var kargo = db.GelenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            db.GelenKargoTakip.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("gelenkargolarılistele");
        }
        public ActionResult sil2(int id)
        {
            var kargo = db.KargoFirmaları.Where(x => x.Id == id).FirstOrDefault();
            db.KargoFirmaları.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("kargolistele");
        }
        public ActionResult sil3(int id)
        {
            var kargo = db.Kullanıcılar.Where(x => x.Id == id).FirstOrDefault();
            db.Kullanıcılar.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("kullanicilistele");
        }
        public ActionResult sil4(int id)
        {
            var kargo = db.Urunler.Where(x => x.Id == id).FirstOrDefault();
            db.Urunler.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("urunlerlistele");
        }
        public ActionResult sil5(int id)
        {
            var kargo = db.Sirketler.Where(x => x.Id == id).FirstOrDefault();
            db.Sirketler.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("sirketlerlistele");
        }
        public ActionResult sil6(int id)
        {
            var kargo = db.gidenUrunlerBilgi.Where(x => x.Id == id).FirstOrDefault();
            db.gidenUrunlerBilgi.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("gidenurunlerlistele");
        }

        public ActionResult gelenkargolarılistele()
        {
            string userEmail = User.Identity.Name; // Oturum açmış kullanıcının e-posta adresini al
            var user = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == userEmail);

            // Kullanıcı bulunduysa ID'sini al
            var userId = user?.Id;

            var list = db.GelenKargoTakip.ToList();

            ViewBag.CurrentUserId = userId; // Kullanıcı ID'sini View'a gönder

            return View(list);
        }

        public ActionResult gidenkargolarılistele()
        {
            string userEmail = User.Identity.Name; // Oturum açmış kullanıcının e-posta adresini al
            var user = db.Kullanıcılar.FirstOrDefault(x => x.kullaniciMailAdresi == userEmail);

            // Kullanıcı bulunduysa ID'sini al
            var userId = user?.Id;

            var list = db.GidenKargoTakip.ToList();

            ViewBag.CurrentUserId = userId; // Kullanıcı ID'sini View'a gönder

            return View(list);
        }


        public ActionResult KargoKapağılistele( int id)
        {
            var detay = db.GidenKargoTakip.Where(x => x.Id == id).ToList();
            return View(detay);
        }
        public ActionResult gelenKargoEkle()
        {
           
            List<SelectListItem> kargolar = (from a in db.KargoFirmaları.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = a.KargoAdi,
                                                 Value = a.Id.ToString()
                                             }).ToList();
            ViewBag.kargolar = kargolar;
            List<SelectListItem> urunler = (from a in db.Urunler.ToList()
                                            select new SelectListItem
                                            {
                                                Text = a.urunAdi,
                                                Value = a.Id.ToString()
                                            }).ToList();
            ViewBag.urunler = urunler;
            List<SelectListItem> kullanıcılar = (from a in db.Kullanıcılar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.kullaniciAdiSoyadi,
                                                     Value = a.Id.ToString()
                                                 }).ToList();
            ViewBag.kullanıcılar = kullanıcılar;

            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            List<SelectListItem> kullanıcılar1 = (from a in db.Kullanıcılar.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = a.kullaniciAdiSoyadi,
                                                      Value = a.Id.ToString()
                                                  }).ToList();
            ViewBag.kullanıcılar1 = kullanıcılar1;
            return View();
        }
        [HttpPost]
        public ActionResult GelenKargoEkle(int kargoID, int teslimAlanID, DateTime gelenTarih, string irsaliyeNo, string durum, int kontrolEdenID, int gonderenSirketID, int[] urunID)
        {
            foreach (var id in urunID)
            {
                var yeniKargo = new GelenKargoTakip
                {
                    kargoID = kargoID,
                    urunID = id,
                    teslimAlanID = teslimAlanID,
                    gelenTarih = gelenTarih,
                    irsaliyeNo = irsaliyeNo,
                    durum = durum,
                    kontrolEdenID = kontrolEdenID,
                    gonderenSirketID = gonderenSirketID
                };

                // Veritabanına kaydet
                db.GelenKargoTakip.Add(yeniKargo);
            }

            db.SaveChanges();

            return RedirectToAction("GelenKargolariListele");
        }

        public ActionResult gidenKargoEkle()
        {

            List<SelectListItem> kargolar = (from a in db.KargoFirmaları.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = a.KargoAdi,
                                                 Value = a.Id.ToString()
                                             }).ToList();
            ViewBag.kargolar = kargolar;
            List<SelectListItem> urunler = (from a in db.gidenUrunlerBilgi.ToList()
                                            select new SelectListItem
                                            {
                                                Text = a.GidenurunAdi + " -- " + a.GidenurunSeriNo,
                                                Value = a.Id.ToString()                                                
                                            }).ToList();
            ViewBag.urunler = urunler;
            List<SelectListItem> kullanıcılar = (from a in db.Kullanıcılar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.kullaniciAdiSoyadi,
                                                     Value = a.Id.ToString()
                                                 }).ToList();
            ViewBag.kullanıcılar = kullanıcılar;

            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            return View();
        }
        [HttpPost]
        public ActionResult gidenKargoEkle(int kargoID, int gidenFirmaID, DateTime gonderilenTarih, string irsaliyeNo, string acıklama, int gonderenKisi, int[] urunID)
        {
            foreach (var id in urunID)
            {
                var yeniKargo1 = new GidenKargoTakip
                {
                    kargoID = kargoID,
                    urunID = id,
                    gidenFirmaID = gidenFirmaID,
                    gonderilenTarih = gonderilenTarih,
                    irsaliyeNo = irsaliyeNo,
                    acıklama = acıklama,
                    gonderenKisi = gonderenKisi,
                };

                // Veritabanına kaydet
                db.GidenKargoTakip.Add(yeniKargo1);
            }

            db.SaveChanges();
            return RedirectToAction("gidenkargolarılistele");
        }


        [HttpPost]
        public ActionResult kargoekle (KargoFirmaları data)
        {
            db.KargoFirmaları.Add(data);
            db.SaveChanges();
            return RedirectToAction("gelenKargoEkle");
        }
        public ActionResult kargoekle ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult urunekle(Urunler data)
        {
            db.Urunler.Add(data);
            db.SaveChanges();

            return RedirectToAction("gelenKargoEkle");
        }
        public ActionResult urunekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult kullanıcı(Kullanıcılar data)
        {
            // Kullanıcı Rol'ünü otomatik olarak "B" yap
            data.Rol = "B";

            // Kullanıcıyı veritabanına kaydet
            db.Kullanıcılar.Add(data);
            db.SaveChanges();

            // Yönlendirme işlemi
            return RedirectToAction("gelenKargoEkle");
        }

        public ActionResult kullanıcı()
        {
            return View();
        }
       public ActionResult kullanicilistele()
        {
            var list = db.Kullanıcılar.ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult sirketler(Sirketler data)
        {
            db.Sirketler.Add(data);
            db.SaveChanges();

            return RedirectToAction("gidenKargoEkle");
        }
        public ActionResult sirketler()
        {
            return View();
        }
        public ActionResult guncelle(int id)
        {
            var guncelle = db.GelenKargoTakip.Where(x => x.Id == id).FirstOrDefault();

            List<SelectListItem> kargolar = (from a in db.KargoFirmaları.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = a.KargoAdi,
                                                 Value = a.Id.ToString()
                                             }).ToList();
            ViewBag.kargolar = kargolar;
            List<SelectListItem> urunler = (from a in db.Urunler.ToList()
                                            select new SelectListItem
                                            {
                                                Text = a.urunAdi,
                                                Value = a.Id.ToString()
                                            }).ToList();
            ViewBag.urunler = urunler;
            List<SelectListItem> kullanıcılar = (from a in db.Kullanıcılar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.kullaniciAdiSoyadi,
                                                     Value = a.Id.ToString()
                                                 }).ToList();
            ViewBag.kullanıcılar = kullanıcılar;

            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            List<SelectListItem> kullanıcılar1 = (from a in db.Kullanıcılar.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = a.kullaniciAdiSoyadi,
                                                      Value = a.Id.ToString()
                                                  }).ToList();
            ViewBag.kullanıcılar1 = kullanıcılar1;
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult guncelle(GelenKargoTakip model)
        {
            var kargoHareket = db.GelenKargoTakip.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.kargoID = model.kargoID;
            kargoHareket.urunID = model.urunID;
            kargoHareket.teslimAlanID = model.teslimAlanID;
            kargoHareket.gelenTarih = model.gelenTarih;
            kargoHareket.irsaliyeNo = model.irsaliyeNo;
            kargoHareket.durum = model.durum;
            kargoHareket.kontrolEdenID = model.kontrolEdenID;
            kargoHareket.gonderenSirketID = model.gonderenSirketID;
            db.SaveChanges();
            return RedirectToAction("gelenkargolarılistele");

        }

        public ActionResult guncelle1(int id)
        {
            var guncelle1 = db.GidenKargoTakip.Where(x => x.Id == id).FirstOrDefault();
            List<SelectListItem> kargolar = (from a in db.KargoFirmaları.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = a.KargoAdi,
                                                 Value = a.Id.ToString()
                                             }).ToList();
            ViewBag.kargolar = kargolar;
            List<SelectListItem> urunler = (from a in db.gidenUrunlerBilgi.ToList()
                                            select new SelectListItem
                                            {
                                                Text = a.GidenurunAdi,
                                                Value = a.Id.ToString()
                                            }).ToList();
            ViewBag.urunler = urunler;
            List<SelectListItem> kullanıcılar = (from a in db.Kullanıcılar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.kullaniciAdiSoyadi,
                                                     Value = a.Id.ToString()
                                                 }).ToList();
            ViewBag.kullanıcılar = kullanıcılar;

            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            return View(guncelle1);
        }
        [HttpPost]
        public ActionResult guncelle1(GidenKargoTakip model)
        {
            var kargoHareket = db.GidenKargoTakip.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.kargoID = model.kargoID;
            kargoHareket.gidenFirmaID = model.gidenFirmaID;
            kargoHareket.urunID = model.urunID;
            kargoHareket.gonderilenTarih = model.gonderilenTarih;
            kargoHareket.irsaliyeNo = model.irsaliyeNo;
            kargoHareket.gonderenKisi = model.gonderenKisi;
            kargoHareket.acıklama = model.acıklama;
            db.SaveChanges();
            return RedirectToAction("gidenkargolarılistele");
            return View();
        }
        public ActionResult kargolistele()
        {
            var list = db.KargoFirmaları.ToList();
            return View(list);
        }
 
        public ActionResult sirketlerlistele()
        {
            var list = db.Sirketler.ToList();
            return View(list);
        }
        public ActionResult urunlerlistele()
        {
            var list = db.Urunler.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult gidenurunekle(gidenUrunlerBilgi data)
        {
            db.gidenUrunlerBilgi.Add(data);
            db.SaveChanges();

            return RedirectToAction("gelenKargoEkle");
        }
        public ActionResult gidenurunekle()
        {
            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            return View();
        }
        public ActionResult gidenurunlerlistele()
        {
            var list = db.gidenUrunlerBilgi.ToList();
            return View(list);
        }
        public ActionResult guncelle3(int id)
        {
            var guncelle = db.Sirketler.Where(x => x.Id == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult guncelle3(Sirketler model)
        {
            var kargoHareket = db.Sirketler.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.sirketAdi = model.sirketAdi;
            kargoHareket.sirketAdresi = model.sirketAdresi;
            kargoHareket.vergiNo = model.vergiNo;
            kargoHareket.vergiDairesi = model.vergiDairesi;
            kargoHareket.telefonNo = model.telefonNo;
            kargoHareket.ilgiliKisi = model.ilgiliKisi;
            db.SaveChanges();
            return RedirectToAction("sirketlerlistele");

        }
        public ActionResult guncelle4(int id)
        {
            var guncelle = db.Kullanıcılar.Where(x => x.Id == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult guncelle4(Kullanıcılar model)
        {
            var kargoHareket = db.Kullanıcılar.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.kullaniciAdiSoyadi = model.kullaniciAdiSoyadi;
            kargoHareket.kullaniciMailAdresi = model.kullaniciMailAdresi;
            kargoHareket.Sifre = model.Sifre;
            kargoHareket.Rol = model.Rol;
            db.SaveChanges();
            return RedirectToAction("kullanicilistesi");

        }
        public ActionResult guncelle5(int id)
        {
            var guncelle = db.gidenUrunlerBilgi.Where(x => x.Id == id).FirstOrDefault();

            List<SelectListItem> sirketler = (from a in db.Sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketAdi,
                                                  Value = a.Id.ToString()
                                              }).ToList();
            ViewBag.sirketler = sirketler;
            return View(guncelle);

        }
        [HttpPost]
        public ActionResult guncelle5(gidenUrunlerBilgi model)
        {
            var kargoHareket = db.gidenUrunlerBilgi.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.GidenurunAdi = model.GidenurunAdi;
            kargoHareket.GidenurunSeriNo = model.GidenurunSeriNo;
            kargoHareket.gidenFirma = model.gidenFirma;
            db.SaveChanges();
            return RedirectToAction("gidenurunlerlistele");

        }
        public ActionResult guncelle6(int id)
        {
            var guncelle = db.KargoFirmaları.Where(x => x.Id == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult guncelle6(KargoFirmaları model)
        {
            var kargoHareket = db.KargoFirmaları.Find(model.Id);
            kargoHareket.Id = model.Id;
            kargoHareket.KargoAdi = model.KargoAdi;
            kargoHareket.KargoilgiliKisi = model.KargoilgiliKisi;
            kargoHareket.KargoTelNo = model.KargoTelNo;
            db.SaveChanges();
            return RedirectToAction("kargolistele");

        }

    }

}