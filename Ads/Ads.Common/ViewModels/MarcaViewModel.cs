using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class MarcaViewModel
    {
        public MarcaViewModel()
        {
        }

        public MarcaViewModel(marcas marca)
        {
            this.Id = marca.Id;
            this.Name = marca.name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
