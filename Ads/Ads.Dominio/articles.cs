namespace Ads.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class articles : EntityBase
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

    public class moto : modeloAtributo
    {
    }

    public class auto : modeloAtributo
    {
        public int tipo { get; set; }
    }

    public class camion : vehiculo
    {
    }

    public class vehiculo : precioAtributo
    {
        public int marca { get; set; }
        public int anio { get; set; }
        public string kilometraje { get; set; }
        public string vin { get; set; }
        public int condicion { get; set; }
    }

    public class modeloAtributo : vehiculo
    {
        public int modelo { get; set; }
    }

    public class departamento_venta : propiedad
    {
    }

    public class departamento_alquiler : propiedad
    {
    }

    public class propiedad : precioAtributo
    {
        public int amueblado { get; set; }
        public int cuartos { get; set; }
        public int cuartos_banio { get; set; }
        public int comision { get; set; }
        public string metros { get; set; }
    }

    public class servicio : propiedad
    {

    }

    public class ModelEstandar : precioAtributo
    {
    }

    public class precioAtributo : articles
    {
        public decimal precio { get; set; }
    }

    public class tipoAtributo : precioAtributo
    {
        public int tipo { get; set; }
    }

}