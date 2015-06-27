namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class tipos : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int articleType_id { get; set; }

        public virtual articleTypes articleTypes { get; set; }
    }
}
