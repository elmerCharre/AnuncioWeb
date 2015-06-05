namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class customers : EntityBase
    {
        public customers()
        {
            articles = new HashSet<articles>();
        }

        [Required]
        [StringLength(100)]
        public string fullname { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(40)]
        public string phone { get; set; }

        [StringLength(80)]
        public string occupation { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        public virtual ICollection<articles> articles { get; set; }
    }
}
