using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ModeloViewModel
    {
        public ModeloViewModel()
        {
        }

        public ModeloViewModel(modelos modelo)
        {
            this.Id = modelo.Id;
            this.Name = modelo.name;
            this.Marca_Id = modelo.marca_id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Marca_Id { get; set; }
    }
}
