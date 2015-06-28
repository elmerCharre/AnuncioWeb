using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class ModeloService : IDisposable
    {
        private IRepository<modelos> _modeloRepository;

        public ModeloService(IRepository<modelos> modeloRepository)
        {
            _modeloRepository = modeloRepository;
        }

        public void Dispose()
        {
            _modeloRepository = null;
        }
    }
}
