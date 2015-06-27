using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class RelationshipMarcaService : IDisposable
    {
        private IRepository<relationship_marca> _relationMarcaRepository;

        public RelationshipMarcaService(IRepository<relationship_marca> relationMarcaRepository)
        {
            _relationMarcaRepository = relationMarcaRepository;
        }

        public void Dispose()
        {
            _relationMarcaRepository = null;
        }
    }
}
