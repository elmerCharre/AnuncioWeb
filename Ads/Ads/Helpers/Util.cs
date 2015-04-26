using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Ads.Helpers
{
    public class Util
    {
        public static string getCurrentUserName() {
            return HttpContext.Current.User.Identity.Name;
        }
    }
}