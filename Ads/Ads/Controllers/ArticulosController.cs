using Ads.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class ArticulosController : Controller
    {
        // GET: Articulos
        public ActionResult Crear(ArticleViewModel articulo)
        {
            return View();
        }
    }
}