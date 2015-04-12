using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ads.Dominio;

namespace Ads.Common.ViewModels
{
    public class ResourceViewModel
    {
        public ResourceViewModel()
        {
        }

        public ResourceViewModel(resource res)
        {
            this.id = res.Id;
            this.path = res.path;
            this.type = res.type;
            this.advertising_id = res.advertising_id;
        }

        public int id { get; set; }
        public string path { get; set; }
        public string type { get; set; }
        public int advertising_id { get; set; }
    }
}
