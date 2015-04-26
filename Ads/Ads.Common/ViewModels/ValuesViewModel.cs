using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class ValuesViewModel
    {
        public ValuesViewModel()
        {
        }

        public ValuesViewModel(fields_value val)
        {
            this.ads_id = val.ads_id;
            this.field_id = val.field_id;
            this.field_label = "";
            this.value = val.value;
        }

        [Required]
        public int ads_id { get; set; }

        [Required]
        public int field_id { get; set; }

        public string field_label { get; set; }

        [Required]
        [StringLength(50)]
        public string value { get; set; }
    }
}
