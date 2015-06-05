namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class categories : EntityBase
    {
        public categories()
        {
            articles = new HashSet<articles>();
            articleTypes = new HashSet<articleTypes>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int status { get; set; }

        public virtual ICollection<articles> articles { get; set; }

        public virtual ICollection<articleTypes> articleTypes { get; set; }
    }
}
