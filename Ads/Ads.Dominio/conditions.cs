namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class conditions : EntityBase
    {
        public conditions()
        {
            relationship_condition = new HashSet<relationship_condition>();
        }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<relationship_condition> relationship_condition { get; set; }
    }
}
