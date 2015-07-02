using System;
using System.Collections.Generic;
using System.Linq;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            this.date = DateTime.Now;
        }

        public ArticleViewModel(articles article)
        {
            this.id = article.Id;
            this.title = article.title;
            this.detail = article.detail;
            this.price = article.price;
            this.date = article.date;
            this.category_Id = article.category_Id;
            this.customer_id = article.customer_id;
            this.resources = article.resources;
            this.articleType = article.GetType().BaseType.Name;
        }

        public int id { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string detail { get; set; }

        public int customer_id { get; set; }

        public int? category_Id { get; set; }

        [Display(Name = "Precio")]
        public decimal price { get; set; }

        public DateTime date { get; set; }

        public ICollection<resources> resources { get; set; }
        
        public string articleType { get; set; }
    }
}
