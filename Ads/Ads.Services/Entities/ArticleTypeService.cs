using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class ArticleTypeService : IDisposable
    {
        private IRepository<articleTypes> _typeRepository;

        public ArticleTypeService(IRepository<articleTypes> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public articleTypes getArticleType(string type)
        {
            return _typeRepository.Get().FirstOrDefault(x => x.type == type);
        }

        public articleTypes getArticleType(int id)
        {
            return _typeRepository.Get().FirstOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            _typeRepository = null;
        }
    }
}
