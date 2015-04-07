using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ads.Dominio;
using Ads.Repository;
using Ads.Services.Entities;

namespace Ads.Controllers
{
    public class advertisingsController : Controller
    {
        private AdvertisingService _advertisingService;

        public advertisingsController(AdvertisingService advertisingservice)
        {
            _advertisingService = advertisingservice;
        }

        // GET: advertisings
        public ActionResult Index()
        {
            return View(db.advertising.ToList());
        }

        // GET: advertisings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advertising advertising = db.advertising.Find(id);
            if (advertising == null)
            {
                return HttpNotFound();
            }
            return View(advertising);
        }

        // GET: advertisings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: advertisings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,detail,price")] advertising advertising)
        {
            if (ModelState.IsValid)
            {
                db.advertising.Add(advertising);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertising);
        }

        // GET: advertisings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advertising advertising = db.advertising.Find(id);
            if (advertising == null)
            {
                return HttpNotFound();
            }
            return View(advertising);
        }

        // POST: advertisings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,detail,price")] advertising advertising)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertising).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertising);
        }

        // GET: advertisings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advertising advertising = db.advertising.Find(id);
            if (advertising == null)
            {
                return HttpNotFound();
            }
            return View(advertising);
        }

        // POST: advertisings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            advertising advertising = db.advertising.Find(id);
            db.advertising.Remove(advertising);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
