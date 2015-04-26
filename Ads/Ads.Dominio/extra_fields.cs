namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("extra_fields")]
    public partial class extra_fields : EntityBase
    {
        public extra_fields()
        {
            fields_value = new HashSet<fields_value>();
        }

        [Required]
        [StringLength(50)]
        public string input { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string label { get; set; }

        public int subtype_id { get; set; }

        public int sort { get; set; }

        public int? parent_field { get; set; }

        public virtual subtype subtype { get; set; }

        public virtual ICollection<fields_value> fields_value { get; set; }
    }
}
