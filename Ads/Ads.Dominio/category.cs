namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("category")]
    public partial class category : EntityBase
    {
        public category()
        {
            subtype = new HashSet<subtype>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int status { get; set; }

        public virtual ICollection<subtype> subtype { get; set; }
    }
}
