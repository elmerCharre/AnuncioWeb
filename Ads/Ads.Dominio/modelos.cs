namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class modelos : EntityBase
    {

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int marca_id { get; set; }

        public virtual marcas marcas { get; set; }
    }
}
