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
            contacts = new HashSet<contacts>();
        }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string detail { get; set; }

        public int customer_id { get; set; }

        public int? category_Id { get; set; }

        public decimal price { get; set; }

        public DateTime date { get; set; }

        public virtual categories categories { get; set; }

        public virtual customers customers { get; set; }

        public virtual ICollection<resources> resources { get; set; }

        public virtual ICollection<contacts> contacts { get; set; }
    }

    public class vehiculo : tipoAtributo
    {
        public int marca { get; set; }
        public int modelo { get; set; }
        public int anio { get; set; }
        public string kilometraje { get; set; }
        public string vin { get; set; }
        public int condicion { get; set; }
    }

    public class moto : vehiculo
    {
    }

    public class auto : vehiculo
    {
    }

    public class camion : vehiculo
    {
    }

    public class depa_venta : propiedad
    {
    }

    public class depa_alquiler : propiedad
    {
    }

    public class temp_alquiler : propiedad
    {
    }

    public class propiedad : articles
    {
        public int amueblado { get; set; }
        public int cuartos { get; set; }
        public int cuartos_banio { get; set; }
        public int comision { get; set; }
        public string metros { get; set; }
    }

    public class empleo : tipoAtributo
    {
        public int opcion_empleo { get; set; }
        public string compania { get; set; }
        public int tiempo { get; set; }
        public int experiencia { get; set; }
        public int pago { get; set; }
        public decimal salario { get; set; }
    }

    public class oferta : empleo
    {
    }

    public class busqueda : empleo
    {
    }

    public class servicio : tipoAtributo
    {
    }

    public class model : articles
    {
    }

    public class tipoAtributo : articles
    {
        public int tipo { get; set; }
    }

}