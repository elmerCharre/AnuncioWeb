namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("category")]
    public class category : EntityBase
    {
        public category()
        {
            article = new HashSet<article>();
            articleType = new HashSet<articleType>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int status { get; set; }

        public virtual ICollection<article> article { get; set; }

        public virtual ICollection<articleType> articleType { get; set; }
    }
}
