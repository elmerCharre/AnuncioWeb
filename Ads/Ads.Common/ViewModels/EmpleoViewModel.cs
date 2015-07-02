using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class EmpleoViewModel : ArticleViewModel
    {
        public EmpleoViewModel()
        {
        }

        public EmpleoViewModel(empleo entity)
        {
            this.id = entity.Id;
            this.title = entity.title;
            this.detail = entity.detail;
            this.price = entity.price;
            this.date = entity.date;
            this.customer_id = entity.customer_id;
            this.category_Id = entity.category_Id;
            this.resources = entity.resources;
            this.opcion_empleo = entity.opcion_empleo;
            this.compania = entity.compania;
            this.tiempo = entity.tiempo;
            this.experiencia = entity.experiencia;
            this.pago = entity.pago;
            this.salario = entity.salario;
            this.tipo = entity.tipo;
        }

        [Required]
        [Display(Name = "En este anuncio")]
        public int opcion_empleo { get; set; }

        [Required]
        [Display(Name = "Nombre de la Compañia")]
        public string compania { get; set; }

        [Display(Name = "Tipo de Posición")]
        public int tiempo { get; set; }

        [Display(Name = "Experiencia (años)")]
        public int experiencia { get; set; }

        [Required]
        [Display(Name = "Forma de Pago")]
        public int pago { get; set; }

        [Required]
        [Display(Name = "Salario (S/.)")]
        public decimal salario { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public int tipo { get; set; }
    }
}
