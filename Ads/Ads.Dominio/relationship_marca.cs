namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class relationship_marca : EntityBase
    {
        public int marca_id { get; set; }

        public int articleType_id { get; set; }

        public virtual articleTypes articleTypes { get; set; }

        public virtual marcas marcas { get; set; }
    }
}
