namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("resource")]
    public class resource : EntityBase
    {

        [Required]
        [StringLength(100)]
        public string path { get; set; }

        [Required]
        [StringLength(20)]
        public string type { get; set; }

        public int article_id { get; set; }

        public virtual article article { get; set; }
    }
}
