using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class ArticleTypeService
    {
        private IRepository<articleTypes> _typeRepository;

        public ArticleTypeService(IRepository<articleTypes> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public void Create(articleTypes typeRepository)
        {
            _typeRepository.Create(typeRepository);
        }

        public void Dispose()
        {
            _typeRepository = null;
        }
    }
}
