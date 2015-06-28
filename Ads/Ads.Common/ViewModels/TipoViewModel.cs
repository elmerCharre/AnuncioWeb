using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class TipoViewModel
    {
        public TipoViewModel()
        {
        }

        public TipoViewModel(tipos tipo)
        {
            this.Id = tipo.Id;
            this.Name = tipo.name;
            this.ArticleType_Id = tipo.articleType_id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ArticleType_Id { get; set; }
    }
}
