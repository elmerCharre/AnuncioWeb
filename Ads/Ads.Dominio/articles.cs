namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class articles : EntityBase
    {
        public articles()
        {
            resources = new HashSet<resources>();
        }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string detail { get; set; }

        public int customer_id { get; set; }

        public int? category_Id { get; set; }

        public virtual categories categories { get; set; }

        public virtual customers customers { get; set; }

        public virtual ICollection<resources> resources { get; set; }
    }

    public class moto : articles
    {
        public decimal price_moto { get; set; }

        public string vin { get; set; }
    }

    public class auto : articles
    {
        public decimal price_auto { get; set; }
        public string kilometraje { get; set; }
    }

}