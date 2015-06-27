using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class MarcaService : IDisposable
    {
        private IRepository<marcas> _marcaRepository;

        public MarcaService(IRepository<marcas> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public void Dispose()
        {
            _marcaRepository = null;
        }
    }
}
