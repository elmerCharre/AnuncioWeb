using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ads.Dominio;

namespace Ads.Common.ViewModels
{
    public class AdvertisingViewModel
    {
        public AdvertisingViewModel()
        {
        }

        public AdvertisingViewModel(advertising advList)
        {
            this.id = advList.Id;
            this.title = advList.title;
            this.title = advList.detail;
            this.price = advList.price;
            this.customer_id = advList.customer_id;
            this.category_id = advList.category_id;
            this.subtype_id = advList.subtype_id;
            this.resource = advList.resource;
        }

        public int id { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public decimal? price { get; set; }
        public int customer_id { get; set; }
        public int category_id { get; set; }
        public int subtype_id { get; set; }
        public ICollection<resource> resource { get; set; }
    }
}
