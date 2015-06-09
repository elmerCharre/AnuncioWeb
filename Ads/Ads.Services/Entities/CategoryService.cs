using System;
using System;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Linq;

namespace Ads.Services.Entities
{
    public class CategoryService : IDisposable
    {
        private IRepository<categories> _categoryRepository;

        public CategoryService(IRepository<categories> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Create(categories categoryRepository)
        {
            _categoryRepository.Create(categoryRepository);
        }

        public void Dispose()
        {
            _categoryRepository = null;
        }
    }
}
