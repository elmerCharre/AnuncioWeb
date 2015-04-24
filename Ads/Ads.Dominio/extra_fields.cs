namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("extra_fields")]
    public partial class extra_fields : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string input { get; set; }

        [Required]
        [StringLength(50)]
        public string label { get; set; }

        public int subtype_id { get; set; }

        public int sort { get; set; }

        public int? parent_field { get; set; }

        public virtual subtype subtype { get; set; }
    }
}
