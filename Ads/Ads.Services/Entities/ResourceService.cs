using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class ResourceService : IDisposable
    {
        private IRepository<resources> _resourceRepository;

        public ResourceService(IRepository<resources> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public void Create(ResourceViewModel resourceRepository)
        {
            var res = new resources
            {
                article_id = resourceRepository.article_id,
                path = resourceRepository.path,
                type = resourceRepository.type
            };
            _resourceRepository.Create(res);
        }

        public void Dispose()
        {
            _resourceRepository = null;
        }
    }
}
