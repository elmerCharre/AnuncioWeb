using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class AutoViewModel : ArticleViewModel
    {
        public AutoViewModel()
        {
        }

        public AutoViewModel(auto entity)
        {
            this.id = entity.Id;
            this.title = entity.title;
            this.detail = entity.detail;
            this.price = entity.price;
            this.date = entity.date;
            this.customer_id = entity.customer_id;
            this.category_Id = entity.category_Id;
            this.resources = entity.resources;
            this.marca = entity.marca;
            this.modelo = entity.modelo;
            this.tipo = entity.tipo;
            this.anio = entity.anio;
            this.kilometraje = entity.kilometraje;
            this.vin = entity.vin;
            this.condicion = entity.condicion;
        }

        [Required]
        [Display(Name = "Marca")]
        public int marca { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int modelo { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public int tipo { get; set; }
        
        [Display(Name = "Año")]
        public int anio { get; set; }

        [Display(Name = "Kilometraje")]
        public string kilometraje { get; set; }

        [Display(Name = "VIN")]
        public string vin { get; set; }

        [Display(Name = "Condición")]
        public int condicion { get; set; }
    }
}
