using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
        }

        public ArticleViewModel(articles advList)
        {
            this.id = advList.Id;
            this.title = advList.title;
            this.detail = advList.detail;
            this.category_id = advList.category_Id;
            this.customer_id = advList.customer_id;
            this.resource = advList.resources;
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

        [Column(TypeName = "int")]
        public int? category_id { get; set; }

        [Column(TypeName = "int")]
        public int customer_id { get; set; }
        
        public string category_name { get; set; }

        public string subtype_name { get; set; }

        public ICollection<resources> resource { get; set; }
    }
}
