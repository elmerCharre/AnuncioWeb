using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
        }

        public CustomerViewModel(customers model)
        {
            this.Id = model.Id;
            this.Email = model.email;
            this.FullName = model.fullname;
            this.Phone = model.phone;
            this.Occupation = model.occupation;
            this.Address = model.address;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
    }
}
