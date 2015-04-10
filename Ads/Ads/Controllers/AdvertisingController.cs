using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Ads.Services.Entities;
using Ads.Common.ViewModels;

namespace Ads.Controllers
{
    public class AdvertisingController : Controller
    {
        private AdvertisingService _AdvertisingService;

        public AdvertisingController(AdvertisingService service)
        {
            _AdvertisingService = service;
        }

        // GET: Advertising
        public ActionResult Index()
        {
            try
            {
                var ads = this._AdvertisingService.GetAll();
                ViewBag.Titulo = "Anuncios";
                return View(ads);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", "Cliente no registrado");
                return View(new List<AdvertisingViewModel>());
            }
            catch (Exception ex)
            {
                throw; // redirigir a una pagina de error
            }
        }

        // GET: Advertising/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //AdvertisingViewModel advertisingViewModel = db.AdvertisingViewModels.Find(id);
            //if (advertisingViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // GET: Advertising/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Anuncio";
            return View(new AdvertisingViewModel());
        }

        // POST: Advertising/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,detail,price,customer_id")] AdvertisingViewModel advertisingViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.AdvertisingViewModels.Add(advertisingViewModel);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Advertising/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //AdvertisingViewModel advertisingViewModel = db.AdvertisingViewModels.Find(id);
            //if (advertisingViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Advertising/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,detail,price,customer_id")] AdvertisingViewModel advertisingViewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(advertisingViewModel).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View();
        }

        // GET: Advertising/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //AdvertisingViewModel advertisingViewModel = db.AdvertisingViewModels.Find(id);
            //if (advertisingViewModel == null)
            //{
            //   return HttpNotFound();
            //}
            return View();
        }

        // POST: Advertising/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //AdvertisingViewModel advertisingViewModel = db.AdvertisingViewModels.Find(id);
            //db.AdvertisingViewModels.Remove(advertisingViewModel);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._AdvertisingService = null;
            }
            base.Dispose(disposing);
        }
    }
}
