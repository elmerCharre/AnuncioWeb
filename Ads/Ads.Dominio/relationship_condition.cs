namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class relationship_condition : EntityBase
    {
        public int condition_id { get; set; }

        public int articleType_id { get; set; }

        public virtual articleTypes articleTypes { get; set; }

        public virtual conditions conditions { get; set; }
    }
}
