using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ArticleTypeViewModel
    {
        public ArticleTypeViewModel()
        {
        }

        public ArticleTypeViewModel(articleTypes type)
        {
            this.Id = type.Id;
            this.Name = type.name;
            this.Category_Id = type.category_id;
            this.Type = type.type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Category_Id { get; set; }
        public string Type { get; set; }
    }
}
