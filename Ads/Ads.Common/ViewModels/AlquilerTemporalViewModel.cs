using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class AlquilerTemporalViewModel : ArticleViewModel
    {
        public AlquilerTemporalViewModel()
        {
        }

        public AlquilerTemporalViewModel(vacaciones_alquiler entity)
        {
            this.id = entity.Id;
            this.title = entity.title;
            this.detail = entity.detail;
            this.price = entity.price;
            this.date = entity.date;
            this.customer_id = entity.customer_id;
            this.category_Id = entity.category_Id;
            this.resources = entity.resources;
            this.amueblado = entity.amueblado;
            this.cuartos = entity.cuartos;
            this.cuartos_banio = entity.cuartos_banio;
            this.comision = entity.comision;
            this.metros = entity.metros;
        }

        [Required]
        [Display(Name = "Amueblado")]
        public int amueblado { get; set; }

        [Required]
        [Display(Name = "Cuartos")]
        public int cuartos { get; set; }

        [Display(Name = "Cuartos de baño")]
        public int cuartos_banio { get; set; }

        [Required]
        [Display(Name = "Comisión Inmobiliaria")]
        public int comision { get; set; }

        [Required]
        [Display(Name = "Metros Cuadrados")]
        public string metros { get; set; }
    }
}
