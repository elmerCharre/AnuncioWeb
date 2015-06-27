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
        
        [Required]
        [Display(Name = "Precio")]
        public decimal precio { get; set; }

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
