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

        public ActionResult Search()
        {
            string text = Request.QueryString["search"];
            var articles = _articleService.getArticlesSearch(text);
            ViewBag.CustomerId = (User.Identity.IsAuthenticated) ? _customerService.getCustomerByEmail(User.Identity.Name).Id : 0;
            return View("Index", articles);
        }

        public ActionResult Message(int id)
        {
            var messages = _articleService.getMessages(id);
            return View("Messages", messages);
        }

        public ActionResult Index(int id = 0)
        {
            ViewBag.Rows = _articleService.getRows();
            id = (id < 1) ? 1 : ((id >= ViewBag.Rows) ? ViewBag.Rows : id);
            ViewBag.CurrentRow = id;
            var articles = _articleService.getArticles(id - 1);
            ViewBag.CustomerId = (User.Identity.IsAuthenticated) ? _customerService.getCustomerByEmail(User.Identity.Name).Id : 0;
            return View(articles);
        }

        public ActionResult Category(int id)
        {
            ViewBag.Rows = _articleService.getRows();
            var articles = _articleService.getArticlesByCategory(id);
            ViewBag.CustomerId = (User.Identity.IsAuthenticated) ? _customerService.getCustomerByEmail(User.Identity.Name).Id : 0;
            return View("Index", articles);
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
            switch (type)
            {
                case "auto":
                case "moto":
                case "camion":
                    ViewBag.marca = new SelectList(_articleService.GetListMarca(articleType_id), "id", "name");
                    ViewBag.condicion = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name");
                    ViewBag.tipo = new SelectList(_articleService.GetListTipo(articleType_id), "id", "name");
                    break;
                case "depa_venta":
                case "depa_alquiler":
                case "temp_alquiler":
                    ViewBag.amueblado = ViewBag.comision = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name");
                    break;
                case "oferta":
                case "busqueda":
                    var helper = new Helpers.HelperAds();
                    ViewBag.opcion_empleo = new SelectList(helper.GetListOpcionEmpleo(), "Id", "Name");
                    ViewBag.tiempo = new SelectList(helper.GetListTiempoEmpleo(), "Id", "Name");
                    ViewBag.pago = new SelectList(helper.GetListPagoEmpleo(), "Id", "Name");
                    ViewBag.tipo = new SelectList(_articleService.GetListTipo(articleType_id), "Id", "Name");
                    break;
            }
            
            ViewBag.categoryID = Convert.ToInt32(Request.QueryString["categories"]);
            ViewBag.customerID = _customerService.getCustomerByEmail(User.Identity.Name).Id;
            return PartialView(type + "/Create");
        }

        public ActionResult Edit(int id)
        {
            var article = _articleService.Get(id);
            int articleType_id = _articleTypeService.getArticleType(article.articleType).Id;
            var viewModel = new ArticleViewModel();
            switch (article.articleType) {
                case "model":
                    viewModel = _articleService.getModel(article.id);
                    break;
                case "auto":
                    var auto = _articleService.getAuto(article.id);
                    ViewBag.marca = new SelectList(_articleService.GetListMarca(articleType_id), "id", "name", auto.marca);
                    ViewBag.condicion = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name", auto.condicion);
                    ViewBag.Tipo = new SelectList(_articleService.GetListTipo(articleType_id), "id", "name", auto.tipo);
                    viewModel = auto;
                    break;
                case "moto":
                    var moto = _articleService.getMoto(article.id);
                    ViewBag.marca = new SelectList(_articleService.GetListMarca(articleType_id), "id", "name", moto.marca);
                    ViewBag.condicion = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name", moto.condicion);
                    viewModel = moto;
                    break;
                case "camion":
                    var camion = _articleService.getMoto(article.id);
                    ViewBag.marca = new SelectList(_articleService.GetListMarca(articleType_id), "id", "name", camion.marca);
                    viewModel = camion;
                    break;
                case "depa_alquiler":
                    var depa_alquiler = _articleService.getDepaAlquiler(article.id);
                    ViewBag.amueblado = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name", depa_alquiler.amueblado);
                    ViewBag.comision = new SelectList(_articleService.GetListCondition(articleType_id), "id", "name", depa_alquiler.comision);
                    viewModel = depa_alquiler;
                    break;
            }
            return View(article.articleType + "/Edit", viewModel);
        }

        /* Methods for Basic Model */
        public ActionResult model(int id)
        {
            var ads = _articleService.getModel(id);
            ViewBag.related = _articleService.getModels(10, id);
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("model/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBasic(BasicViewModel Vmodel, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(Vmodel.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<BasicViewModel, model>();
                var entity = Mapper.Map<BasicViewModel, model>(Vmodel);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return model(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBasic(BasicViewModel Vmodel, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<BasicViewModel, model>();
                var entity = Mapper.Map<BasicViewModel, model>(Vmodel);
                _articleService.Update(entity);
                addResources(Vmodel.id, files);
            }
            return model(Vmodel.id);
        }

        /* Methods for Auto Model */
        public ActionResult auto(int id)
        {
            var ads = _articleService.getAuto(id);
            ViewBag.related = _articleService.getAutos(10, id);
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("auto/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuto(AutoViewModel model, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<AutoViewModel, auto>();
                var entity = Mapper.Map<AutoViewModel, auto>(model);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return auto(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuto(AutoViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<AutoViewModel, auto>();
                var entity = Mapper.Map<AutoViewModel, auto>(model);
                _articleService.Update(entity);
                addResources(model.id, files);
            }
            return auto(model.id);
        }

        /* Methods for Moto Model */
        public ActionResult moto(int id)
        {
            var ads = _articleService.getMoto(id);
            ViewBag.related = _articleService.getMotos(10, id);
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("moto/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMoto(MotoViewModel model, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<MotoViewModel, moto>();
                var entity = Mapper.Map<MotoViewModel, moto>(model);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return moto(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMoto(MotoViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<MotoViewModel, moto>();
                var entity = Mapper.Map<MotoViewModel, moto>(model);
                _articleService.Update(entity);
                addResources(model.id, files);
            }
            return moto(model.id);
        }

        /* Methods for Camion Model */
        public ActionResult camion(int id)
        {
            var ads = _articleService.getCamion(id);
            ViewBag.related = _articleService.getCamiones(10, id);
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("camion/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCamion(CamionViewModel model, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<CamionViewModel, camion>();
                var entity = Mapper.Map<CamionViewModel, camion>(model);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return camion(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCamion(CamionViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<CamionViewModel, camion>();
                var entity = Mapper.Map<CamionViewModel, camion>(model);
                _articleService.Update(entity);
                addResources(model.id, files);
            }
            return camion(model.id);
        }

        /* Methods for Depa Alquiler Model */
        public ActionResult depa_alquiler(int id)
        {
            var ads = _articleService.getDepaAlquiler(id);
            ViewBag.related = _articleService.getDepaAlquileres(10, id);
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("depa_alquiler/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlquiler(DepartamentoAlquilerViewModel model, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<DepartamentoAlquilerViewModel, depa_alquiler>();
                var entity = Mapper.Map<DepartamentoAlquilerViewModel, depa_alquiler>(model);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return depa_alquiler(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAlquiler(DepartamentoAlquilerViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<DepartamentoAlquilerViewModel, depa_alquiler>();
                var entity = Mapper.Map<DepartamentoAlquilerViewModel, depa_alquiler>(model);
                _articleService.Update(entity);
                addResources(model.id, files);
            }
            return depa_alquiler(model.id);
        }

        /* Methods for Empleo Oferta Model */
        public ActionResult oferta(int id)
        {
            var ads = _articleService.getEmpleoOferta(id);
            ViewBag.related = _articleService.getEmpleoOfertas(10, id);
            var helper = new Helpers.HelperAds();
            ads.opcion_name = helper.GetOpcionEmpleo(ads.opcion_empleo);
            ads.tiempo_name = helper.GetTiempoEmpleo(ads.tiempo);
            ads.pago_name = helper.GetPagoEmpleo(ads.pago);            
            ViewBag.userInfo = _customerService.getCustomerById(ads.customer_id);
            return View("oferta/View", ads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOferta(OfertaEmpleoViewModel model, HttpPostedFileBase[] files)
        {
            int article_id = 0;
            if (ModelState.IsValid)
            {
                var customer = _customerService.getCustomerById(model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
                Mapper.CreateMap<OfertaEmpleoViewModel, oferta>();
                var entity = Mapper.Map<OfertaEmpleoViewModel, oferta>(model);
                article_id = _articleService.Create(entity);
                addResources(article_id, files);
            }
            return oferta(article_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOferta(OfertaEmpleoViewModel model, HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<OfertaEmpleoViewModel, oferta>();
                var entity = Mapper.Map<OfertaEmpleoViewModel, oferta>(model);
                _articleService.Update(entity);
                addResources(model.id, files);
            }
            return oferta(model.id);
        }


        private int addResources(int articleID, HttpPostedFileBase[] files)
        {
            int rows_affected = 0;
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
                    rows_affected += _resourceService.Create(res);
                }
            }
            return rows_affected;
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

        public ActionResult CreateContact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _articleService.CreateContact(model);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
