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

namespace Ads.Controllers
{
    public class AdsController : Controller
    {
        private ArticleService _articleService;
        private ArticleTypeService _articleTypeService;
        private ResourceService _resourceService;
        private CustomerService _customerService;

        public AdsController(ArticleService articleService, ArticleTypeService articleTypeService,
            ResourceService resourceService, CustomerService customerService)
        {
            _articleService = articleService;
            _articleTypeService = articleTypeService;
            _resourceService = resourceService;
            _customerService = customerService;
        }

        //public ActionResult Index()
        //{
        //    string type = Request.QueryString["Type"];
        //    var ads = this._articleService.getListAuto();
        //    ViewBag.Titulo = "Anuncios";
        //    return View(ads);
        //}

        public ActionResult Index(int id)
        {
            var articles = _articleService.getArticlesByCategory(id);
            return View(articles);
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ads = _articleService.Get(id);
            return View(ads);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.categories = new SelectList(_articleService.GetListCategory(), "id", "name");
            return View(new ArticleViewModel());
        }

        public ActionResult CreateModel()
        {
            string type = Request.QueryString["articleType"];
            int articleType_id = _articleTypeService.getArticleType(type).Id;
            ViewBag.marcas = new SelectList(_articleService.GetListMarca(articleType_id), "id", "name");
            ViewBag.condiciones = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name");
            ViewBag.Tipos = new SelectList(_articleService.GetListTipo(articleType_id), "id", "name");

            ViewBag.categoryID = Convert.ToInt32(Request.QueryString["categories"]);
            ViewBag.customerID = _customerService.getCustomerByEmail(User.Identity.Name).Id;
            return PartialView(type + "/Create");
        }

        public JsonResult GetListSubtypeByCategory(int id)
        {
            return Json(_articleService.GetListSubtypeByCategoryAsJson(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListModeloByMarca(int id)
        {
            return Json(_articleService.GetListModeloByMarca(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListArticleTypeByCategory(int id)
        {
            return Json(_articleService.GetListArticleTypeByCategory(id), JsonRequestBehavior.AllowGet);
        }

        public int deleteArticle(int id)
        {
            return _articleService.deleteArticle(id);
        }

        private void addResources(int articleID, HttpPostedFileBase[] files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var res = new ResourceViewModel
                    {
                        article_id = articleID,
                        path = filename,
                        type = file.ContentType
                    };
                    var directory = Path.Combine(Server.MapPath("~/resources"), "resource_" + articleID);
                    Directory.CreateDirectory(directory);
                    var path_file = directory + "/" + filename;
                    file.SaveAs(path_file);
                    _resourceService.Create(res);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuto(AutoViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<AutoViewModel, auto>();
                auto entity = Mapper.Map<AutoViewModel, auto>(model);
                var article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMoto(MotoViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<MotoViewModel, moto>();
                moto entity = Mapper.Map<MotoViewModel, moto>(model);
                var article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return RedirectToAction("Index");
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
