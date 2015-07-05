using System.Linq;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Collections.Generic;
using System;
using System.Web;

namespace Ads.Helpers
{
    public class Util
    {
        public static string getCurrentUserName() {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}