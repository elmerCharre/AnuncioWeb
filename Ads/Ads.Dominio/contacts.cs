namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class contacts : EntityBase
    {
        [Required]
        public int article_id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        [StringLength(300)]
        public string message { get; set; }

        [Required]
        public string email { get; set; }

        public string phone { get; set; }

        public virtual articles articles { get; set; }
    }
}
