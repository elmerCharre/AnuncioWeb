using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
        }

        public ContactViewModel(contacts model)
        {
            this.id = model.Id;
            this.article_id = model.article_id;
            this.name = model.name;
            this.message = model.message;
            this.email = model.email;
            this.phone = model.phone;
        }

        public int id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Mensaje")]
        public string message { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Display(Name = "Tu número de teléfono (opcional)")]
        public string phone { get; set; }

        [Required]
        public int article_id { get; set; }
    }
}
