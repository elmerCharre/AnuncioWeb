using Ads.Dominio;
using Ads.Common.ViewModels;
using System.Collections.Generic;
using System;
using System.Web;

namespace Ads.Helpers
{
    public class HelperAds
    {
        public HelperAds()
        {

        }

        public IEnumerable<ConditionViewModel> GetListOpcionEmpleo()
        {
            var json = new List<ConditionViewModel>();
            json.Add(new ConditionViewModel
            {
                Id = 1,
                Name = "Estoy buscando un empleo"
            });
            json.Add(new ConditionViewModel
            {
                Id = 2,
                Name = "Estoy ofreciendo un empleo"
            });
            return json;
        }

        public string GetOpcionEmpleo(int id)
        {
            if (id == 1) return "Estoy buscando un empleo";
            else return "Estoy ofreciendo un empleo";
        }

        public IEnumerable<ConditionViewModel> GetListTiempoEmpleo()
        {
            var json = new List<ConditionViewModel>();
            json.Add(new ConditionViewModel
            {
                Id = 1,
                Name = "Medio Tiempo"
            });
            json.Add(new ConditionViewModel
            {
                Id = 2,
                Name = "Tiempo completo"
            });
            return json;
        }

        public string GetTiempoEmpleo(int id)
        {
            if (id == 1) return "Medio Tiempo";
            else return "Tiempo completo";
        }

        public IEnumerable<ConditionViewModel> GetListPagoEmpleo()
        {
            var json = new List<ConditionViewModel>();
            json.Add(new ConditionViewModel
            {
                Id = 1,
                Name = "Por Hora"
            });
            json.Add(new ConditionViewModel
            {
                Id = 2,
                Name = "Por Semana"
            });
            json.Add(new ConditionViewModel
            {
                Id = 3,
                Name = "Por Mes"
            });
            json.Add(new ConditionViewModel
            {
                Id = 4,
                Name = "Por Año"
            });
            return json;
        }

        public string GetPagoEmpleo(int id)
        {
            if (id == 1) return "Por Hora";
            else if (id == 2) return "Por Semana";
            else if (id == 3) return "Por Mes";
            else return "Por Año";
        }

    }
}