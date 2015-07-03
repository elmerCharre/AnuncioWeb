using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ads.Services.Entities;
using Ads.Common.ViewModels;
using System.IO;

namespace Ads.Helpers
{
    public class AdsHelper
    {
        private ResourceService _resourceService;

        public AdsHelper(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public int addResources(int articleID, HttpPostedFileBase[] files)
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
                    var directory = Path.Combine(HttpContext.Current.Server.MapPath("~/resources"), "resource_" + articleID);
                    Directory.CreateDirectory(directory);
                    var path_file = directory + "/" + filename;
                    file.SaveAs(path_file);
                    rows_affected += _resourceService.Create(res);
                }
            }
            return rows_affected;
        }
    }
}