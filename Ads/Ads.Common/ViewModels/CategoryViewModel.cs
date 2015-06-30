using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
        }

        public CategoryViewModel(categories category)
        {
            this.Id = category.Id;
            this.Name = category.name;
            this.Order = category.status;
            this.articleTypes = category.articleTypes;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ICollection<articleTypes> articleTypes { get; set; }
    }
}
