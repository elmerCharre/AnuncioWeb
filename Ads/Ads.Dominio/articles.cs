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

    public class moto : vehiculos
    {
    }

    public class auto : vehiculos
    {
    }

    public class camion : vehiculos
    {
    }

    public class vehiculos : articles
    {
        public decimal precio { get; set; }
        public int marca { get; set; }
        public int modelo { get; set; }
        public int tipo { get; set; }
        public int anio { get; set; }
        public string kilometraje { get; set; }
        public string vin { get; set; }
        public int condicion { get; set; }
    }

    public class departamento_venta : propiedades
    {
    }

    public class departamento_alquiler : propiedades
    {
    }

    public class propiedades : articles
    {
        public decimal precio { get; set; }
        public int amueblado { get; set; }
        public int cuartos { get; set; }
        public int cuartos_banio { get; set; }
        public int comision { get; set; }
        public string metros { get; set; }
    }

}