namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class articleTypes : EntityBase
    {
        public articleTypes()
        {
            relationship_condition = new HashSet<relationship_condition>();
            relationship_marca = new HashSet<relationship_marca>();
            tipos = new HashSet<tipos>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int category_id { get; set; }

        public string type { get; set; }

        public virtual categories categories { get; set; }

        public virtual ICollection<relationship_condition> relationship_condition { get; set; }

        public virtual ICollection<relationship_marca> relationship_marca { get; set; }

        public virtual ICollection<tipos> tipos { get; set; }
    }
}
