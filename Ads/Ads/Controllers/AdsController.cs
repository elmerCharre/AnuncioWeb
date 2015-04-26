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
using System.Web.Script.Serialization;
using System.Linq;

namespace Ads.Controllers
{
    public class AdsController : Controller
    {
        private AdvertisingService _AdvertisingService;
        private ResourceService _resourceService;
        private ValuesService _valuesService;
        private CustomerService _customerService;

        public AdsController(AdvertisingService service, ResourceService res, ValuesService valuesService, CustomerService customerService)
        {
            _AdvertisingService = service;
            _resourceService = res;
            _valuesService = valuesService;
            _customerService = customerService;
        }

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

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ads = _AdvertisingService.Get(id);
            return View(ads);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(_AdvertisingService.GetListCategory(), "id", "name");
            ViewBag.subtype_id = new SelectList(_AdvertisingService.GetListSubtypeByCategory(1), "id", "name");
            return View(new AdvertisingViewModel());
        }

        public JsonResult getExtraFieldsAsJson(int id = 0)
        {
            return Json(_AdvertisingService.GetListFieldsAsJson(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListSubtypeByCategory(int id)
        {
            return Json(_AdvertisingService.GetListSubtypeByCategoryAsJson(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetElementsAsJson(int id)
        {
            return Json(_AdvertisingService.GetElementsAsJson(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Advertising/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,title,detail,price,customer_id,category_id,subtype_id")] AdvertisingViewModel advertisingViewModel, HttpPostedFileBase[] files)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var id_ads = _AdvertisingService.Create(advertisingViewModel);

        //        foreach (var file in files)
        //        {
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var filename = Path.GetFileName(file.FileName);
        //                var res = new ResourceViewModel
        //                {
        //                    advertising_id = id_ads,
        //                    path = filename,
        //                    type = file.ContentType
        //                };

        //                var directory = Path.Combine(Server.MapPath("~/resources"), "resource_" + id_ads);
        //                Directory.CreateDirectory(directory);
        //                var path_file = directory + "/" + filename;
        //                file.SaveAs(path_file);

        //                _resourceService.Create(res);
        //            }
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formData, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                var _value = new Dictionary<string, string>();

                foreach (string _key in formData.Keys)
                {
                    _value[_key] = formData[_key];
                }

                var ads = new AdvertisingViewModel
                {
                    category_id = Convert.ToInt16(_value["category_id"]),
                    subtype_id = Convert.ToInt16(_value["subtype_id"]),
                    title = _value["title"],
                    detail = _value["detail"],
                    price = Convert.ToDecimal(_value["price"]),
                    customer_id = _customerService.getCustomer(User.Identity.Name).Id
                };
                var id_ads = _AdvertisingService.Create(ads);

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var res = new ResourceViewModel
                        {
                            advertising_id = id_ads,
                            path = filename,
                            type = file.ContentType
                        };

                        var directory = Path.Combine(Server.MapPath("~/resources"), "resource_" + id_ads);
                        Directory.CreateDirectory(directory);
                        var path_file = directory + "/" + filename;
                        file.SaveAs(path_file);
                        _resourceService.Create(res);
                    }
                }

                foreach (var _object in _value)
                {
                    if (_object.Key.Length > 6 && _object.Key.Substring(0, 6) == "extra_")
                    {
                        var field_value = new ValuesViewModel
                        {
                            ads_id = id_ads,
                            field_id = Convert.ToInt16(_object.Key.Replace("extra_", "")),
                            value = _object.Value
                        };
                        _valuesService.Create(field_value);
                    }
                }

                return RedirectToAction("Index");
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
