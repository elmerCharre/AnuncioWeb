namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("fields_value")]
    public partial class fields_value : EntityBase
    {
        [Required]
        public int ads_id { get; set; }

        [Required]
        public int field_id { get; set; }

        [Required]
        [StringLength(50)]
        public string value { get; set; }

        public virtual advertising advertising { get; set; }

        public virtual extra_fields extra_fields { get; set; }
    }
}
