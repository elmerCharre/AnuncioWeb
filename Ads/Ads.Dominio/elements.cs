namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("elements")]
    public partial class elements : EntityBase
    {
        [Required]
        public int value { get; set; }

        [Required]
        [StringLength(50)]
        public string label { get; set; }

        [Required]
        public int status { get; set; }

        [Required]
        public int fields_id { get; set; }
    }
}
