namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("advertising")]
    public partial class advertising : EntityBase
    {
        public advertising()
        {
            fields_value = new HashSet<fields_value>();
            resource = new HashSet<resource>();
        }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string detail { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? price { get; set; }

        public int customer_id { get; set; }

        public int category_id { get; set; }

        public int subtype_id { get; set; }

        public virtual customer customer { get; set; }

        public virtual ICollection<fields_value> fields_value { get; set; }

        public virtual ICollection<resource> resource { get; set; }
    }
}
