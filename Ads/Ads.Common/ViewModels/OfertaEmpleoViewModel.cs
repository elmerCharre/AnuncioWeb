using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.Dominio;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ads.Common.ViewModels
{
    public class OfertaEmpleoViewModel : EmpleoViewModel
    {
        public OfertaEmpleoViewModel()
        {
        }

        public OfertaEmpleoViewModel(oferta entity)
        {
            this.id = entity.Id;
            this.title = entity.title;
            this.detail = entity.detail;
            this.price = entity.price;
            this.date = entity.date;
            this.customer_id = entity.customer_id;
            this.category_Id = entity.category_Id;
            this.resources = entity.resources;
            this.opcion_empleo = entity.opcion_empleo;
            this.compania = entity.compania;
            this.tiempo = entity.tiempo;
            this.experiencia = entity.experiencia;
            this.pago = entity.pago;
            this.salario = entity.salario;
            this.tipo = entity.tipo;
        }
    }
}