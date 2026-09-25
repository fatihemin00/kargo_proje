using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kargo_takip1.Models;
using System.Collections;

namespace kargo_takip1.Controllers
{
    public class homeController : Controller
    {
        goratakipEntities1 db = new goratakipEntities1();
        // GET: home
        public ActionResult Index()
        {
            var list = db.kargoHareket.ToList();
            return View(list);
        }
        public ActionResult guncelle()
        {
            return View();
        }
        public ActionResult urunEkle()
        {
            List<SelectListItem> kargolar = (from a in db.kargolar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = a.kargoAdi,
                                               Value = a.kargoId.ToString()
                                           }).ToList();
            ViewBag.kargolar = kargolar;
            List<SelectListItem> urunler = (from a in db.urunler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = a.urunAdi,
                                               Value = a.urunId.ToString()
                                           }).ToList();
            ViewBag.urunler = urunler;
            List<SelectListItem> kullanıcılar = (from a in db.kullanıcılar.ToList()
                                            select new SelectListItem
                                            {
                                                Text = a.kullaniciAdi,
                                                Value = a.kullanıcıId.ToString()
                                            }).ToList();
            ViewBag.kullanıcılar = kullanıcılar;
            List<SelectListItem> kullanıcılar1 = (from a in db.kullanıcılar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.kullaniciAdi,
                                                     Value = a.kullanıcıId.ToString()
                                                 }).ToList();
            ViewBag.kullanıcılar1 = kullanıcılar1;

            List<SelectListItem> sirketler = (from a in db.sirketler.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = a.sirketadi,
                                                     Value = a.sirketId.ToString()
                                                 }).ToList();
            ViewBag.sirketler = sirketler;
            List<SelectListItem> sirketler1 = (from a in db.sirketler.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = a.sirketadi,
                                                  Value = a.sirketId.ToString()
                                              }).ToList();
            ViewBag.sirketler1 = sirketler1;


            return View();
        }
        [HttpPost]
        public ActionResult urunEkle(kargoHareket data)
        {
            db.kargoHareket.Add(data);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult kargoekle (kargolar data)
        {
            db.kargolar.Add(data);
            db.SaveChanges();

            return RedirectToAction("urunEkle");
        }

        public ActionResult kargoekle ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult urunekle1(urunler data)
        {
            db.urunler.Add(data);
            db.SaveChanges();

            return RedirectToAction("urunEkle");
        }

        public ActionResult urunekle1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult kullanıcı(kullanıcılar data)
        {
            db.kullanıcılar.Add(data);
            db.SaveChanges();

            return RedirectToAction("urunEkle");
        }

        public ActionResult kullanıcı()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sirketler(sirketler data)
        {
            db.sirketler.Add(data);
            db.SaveChanges();

            return RedirectToAction("urunEkle");
        }

        public ActionResult sirketler()
        {
            return View();
        }
        public ActionResult sil (int id)
        {
            var kargo = db.kargoHareket.Where(x => x.ıd == id).FirstOrDefault();
            db.kargoHareket.Remove(kargo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}