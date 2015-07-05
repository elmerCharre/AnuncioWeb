using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class MotoViewModel : VehiculoViewModel
    {
        public MotoViewModel()
        {
        }

        public MotoViewModel(moto entity)
        {
            this.id = entity.Id;
            this.title = entity.title;
            this.detail = entity.detail;
            this.price = entity.price;
            this.date = entity.date;
            this.customer_id = entity.customer_id;
            this.category_Id = entity.category_Id;
            this.resources = entity.resources;
            this.marca = entity.marca;
            this.modelo = entity.modelo;
            this.anio = entity.anio;
            this.kilometraje = entity.kilometraje;
            this.vin = entity.vin;
            this.condicion = entity.condicion;
            this.articleType = entity.GetType().BaseType.Name;
        }

        public string marca_name { get; set; }
        public string modelo_name { get; set; }
        public string tipo_name { get; set; }
        public string condicion_name { get; set; }
    }
}
