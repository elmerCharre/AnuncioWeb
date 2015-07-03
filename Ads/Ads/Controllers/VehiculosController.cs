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
using AutoMapper;
using Ads.Helpers;

namespace Ads.Controllers
{
    public class VehiculosController : Controller
    {
        private ArticleService _articleService;
        private ResourceService _resourceService;
        private CustomerService _customerService;

        public VehiculosController(ArticleService articleService,
            ResourceService resourceService, CustomerService customerService)
        {
            _articleService = articleService;
            _resourceService = resourceService;
            _customerService = customerService;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._articleService = null;
            }
            base.Dispose(disposing);
        }
    }
}