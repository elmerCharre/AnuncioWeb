using Ads.Common.ViewModels;
using Ads.Dominio;
using Ads.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Services
{

    public class AutoBuilder : IBuilder<AutoViewModel>
    {
        private IRepository<auto> repo;
        public AutoBuilder(IRepository<auto> repo)
        {
            this.repo = repo;
        }
        public bool EsAplicableA(string tipo)
        {
            return (tipo == "auto");
        }

        public AutoViewModel Build()
        {
            var modelo = new AutoViewModel();
            // crear poner tosas las propiedas

            return modelo;
        }
    }

    public interface IBuilder<T>
    {
        bool EsAplicableA(string tipo);
        T Build();
    }
}
