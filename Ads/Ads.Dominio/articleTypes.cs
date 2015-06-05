namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class articleTypes : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int category_id { get; set; }

        public int status { get; set; }

        public virtual categories categories { get; set; }
    }
}
