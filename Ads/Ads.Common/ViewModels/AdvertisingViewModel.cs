using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Título")]
        public string title { get; set; }

        [Display(Name = "Descripción")]
        public string detail { get; set; }

        [Display(Name = "Precio")]
        public decimal? price { get; set; }
        public int customer_id { get; set; }

        [Display(Name = "Categoría")]
        public int category_id { get; set; }

        [Display(Name = "Tipo")]
        public int subtype_id { get; set; }
        public ICollection<resource> resource { get; set; }
    }
}
