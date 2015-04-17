using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Ads.Services.Entities;
using Ads.Common.ViewModels;
using Ads.Dominio;
using System.IO;

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

        [Authorize]
        // GET: Advertising/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Anuncio";
            ViewBag.category_id = new SelectList(_AdvertisingService.GetListCategory(), "id", "name");
            ViewBag.subtype_id = new SelectList(_AdvertisingService.GetListSubtypeByCategory(1), "id", "name");
            return View(new AdvertisingViewModel());
        }

        // POST: Advertising/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,detail,price,customer_id,category_id,subtype_id")] AdvertisingViewModel advertisingViewModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var id_ads = _AdvertisingService.Create(advertisingViewModel);
                var ads = _AdvertisingService.Get(id_ads);

                if (upload != null && upload.ContentLength > 0)
                {
                    var res = new resource()
                    {
                        Id = 0,
                        advertising_id = 0,
                        path = System.IO.Path.GetFileName(upload.FileName),
                        type = upload.ContentType
                    };
                    //using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    //{
                    //    resource.Content = reader.ReadBytes(upload.ContentLength);
                    //}
                    //advertisingViewModel.resource = new List<ResourceViewModel> { resource };
                    ads.resource.Add(res);
                    _AdvertisingService.Update(ads);
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        //public ActionResult Create(FormCollection advertisingViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var trackIds = advertisingViewModel.GetValues("resource");
        //        foreach (string trackId in trackIds)
        //        {
        //            Response.Write(trackId);
        //            playListService.addTrack(playlistId, int.Parse(trackId));
        //        }

        //        var ads = new AdvertisingViewModel()
        //        {
        //            category_id = Convert.ToInt16(advertisingViewModel.Get("category_id")),
        //            subtype_id = Convert.ToInt16(advertisingViewModel.Get("subtype_id")),
        //            title = advertisingViewModel.Get("title").ToString(),
        //            detail = advertisingViewModel.Get("detail").ToString(),
        //            price = Convert.ToDecimal(advertisingViewModel.Get("price")),
        //        };
        //        _AdvertisingService.Create(ads);
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

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
