using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class FieldsViewModel
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string input { get; set; }

        [Required]
        [StringLength(50)]
        public string label { get; set; }

        public int subtype_id { get; set; }

        public int sort { get; set; }

        public int? parent_field { get; set; }

        public ICollection<elements> elements { get; set; }
    }
}
