namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("article")]
    public class article : EntityBase
    {
        public article()
        {
            resource = new HashSet<resource>();
        }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string detail { get; set; }

        public int customer_id { get; set; }

        public int category_id { get; set; }

        public virtual category category { get; set; }

        public virtual customer customer { get; set; }

        public virtual ICollection<resource> resource { get; set; }
    }

    public class moto : article
    {
        public decimal price_moto { get; set; }

        public string vin { get; set; }
    }

    public class auto : article
    {
        public decimal price_auto { get; set; }
        public string kilometraje { get; set; }
    }

}
