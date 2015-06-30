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
        }

        public ArticleViewModel(articles article)
        {
            this.id = article.Id;
            this.title = article.title;
            this.detail = article.detail;
            this.category_Id = article.category_Id;
            this.customer_id = article.customer_id;
            this.resources = article.resources;
            this.articleType = article.GetType().BaseType.Name;
            this.Type = null;
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

        public ICollection<resources> resources { get; set; }
        public string articleType { get; set; }
        public object Type { get; set; }
    }
}
