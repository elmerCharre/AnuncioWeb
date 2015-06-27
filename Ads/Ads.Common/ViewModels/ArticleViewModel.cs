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
            this.category_Id = advList.category_Id;
            this.customer_id = advList.customer_id;
            this.resources = advList.resources;
        }

        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [Column(TypeName = "text")]
        public string detail { get; set; }

        public int customer_id { get; set; }

        public int? category_Id { get; set; }

        public virtual categories categories { get; set; }

        public virtual customers customers { get; set; }

        public ICollection<resources> resources { get; set; }
    }
}
