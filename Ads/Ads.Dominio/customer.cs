namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("customer")]
    public partial class customer : EntityBase
    {

        [Required]
        [StringLength(20)]
        public string username { get; set; }

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
    }
}
