namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("subtype")]
    public partial class subtype : EntityBase
    {

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int category_id { get; set; }

        public int status { get; set; }

        public virtual category category { get; set; }
    }
}
