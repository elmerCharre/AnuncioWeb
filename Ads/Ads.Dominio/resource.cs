namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("resource")]
    public partial class resource : EntityBase
    {

        [Required]
        [StringLength(100)]
        public string path { get; set; }

        [Required]
        [StringLength(20)]
        public string type { get; set; }

        public int advertising_id { get; set; }

        public virtual advertising advertising { get; set; }
    }
}
