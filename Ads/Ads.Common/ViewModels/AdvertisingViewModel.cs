using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            this.category_id = advList.category_id;
            this.subtype_id = advList.subtype_id;
            this.title = advList.title;
            this.detail = advList.detail;
            this.price = advList.price;
            this.customer_id = advList.customer_id;
            this.resource = advList.resource;
            this.category_name = "";
            this.subtype_name = "";
        }

        public int id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [Column(TypeName = "text")]
        public string detail { get; set; }

        [Display(Name = "Precio")]
        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        public int customer_id { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        [Column(TypeName = "int")]
        public int category_id { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        [Column(TypeName = "int")]
        public int subtype_id { get; set; }

        public string category_name { get; set; }

        public string subtype_name { get; set; }

        public ICollection<fields_value> fields_value { get; set; }

        public ICollection<ValuesViewModel> values { get; set; }

        public ICollection<resource> resource { get; set; }
    }
}
