namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class marcas : EntityBase
    {
        public marcas()
        {
            modelos = new HashSet<modelos>();
            relationship_marca = new HashSet<relationship_marca>();
        }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public virtual ICollection<modelos> modelos { get; set; }

        public virtual ICollection<relationship_marca> relationship_marca { get; set; }
    }
}
