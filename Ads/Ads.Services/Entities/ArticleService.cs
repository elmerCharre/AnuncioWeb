using System.Linq;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Collections.Generic;
using System;
using System.Web;

namespace Ads.Services.Entities
{
    public class ArticleService
    {
        private IRepository<article> _articleRepository;
        private IRepository<articleType> _articleTypeRepository;
        private IRepository<customer> _customerRepository;
        private IRepository<category> _categoryRepository;

        public ArticleService(IRepository<article> articleRepository,
            IRepository<articleType> articleTypeRepository, 
            IRepository<customer> customerRepository, IRepository<category> categoryRepository)
        {
            _articleRepository = articleRepository;
            _articleTypeRepository = articleTypeRepository;
            _customerRepository = customerRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ArticleViewModel> GetListByUser(string userName)
        {
            var customer = _customerRepository.Get().FirstOrDefault(c => c.email == userName);
            if (customer == null) throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", userName));
            var AdvertisingLists = _articleRepository.Get().Where(x => x.customer_id == customer.Id).ToList();
            // aqui se tiene que hacer un mapeo del dominio al viewmodel
            return AdvertisingLists.Select(AdsList => new ArticleViewModel(AdsList)).ToList();
        }

        public IEnumerable<ArticleViewModel> GetAll()
        {
            var AdvertisingLists = _articleRepository.Get().ToList();
            return AdvertisingLists.Select(AdsList => new ArticleViewModel(AdsList)).ToList();
        }
         
        public void Dispose()
        {
            _articleRepository = null;
            _customerRepository = null;
        }

        public int Create(ArticleViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            var advertising = new article()
            {
                title = model.title,
                detail = model.detail,
                customer_id = customer.Id,
                //articleType = model.articleType,
                resource = model.resource
            };
            return _articleRepository.Create(advertising);
        }

        public void Update(ArticleViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            var advertising = new article()
            {
                title = model.title,
                detail = model.detail,
                customer_id = customer.Id,
                //articleType = model.articleType,
                resource = model.resource
            };
            _articleRepository.Update(advertising);
        }

        public ArticleViewModel Get(int id)
        {
            var AdsList = _articleRepository.Get().FirstOrDefault(x => x.Id == id);
            if (AdsList == null) throw new InvalidOperationException("Anuncio no encontrado");
                        
            return new ArticleViewModel 
            {
                id = AdsList.Id,
                //category_name = _categoryRepository.Get().FirstOrDefault(c => c.Id == AdsList.category_id).name,
                //subtype_id = AdsList.article_id,
                //subtype_name = _articleRepository.Get().FirstOrDefault(s => s.Id == AdsList.article_id).name,
                //title = AdsList.title,
                //detail = AdsList.detail,
                //customer_id = AdsList.customer_id,
                resource = AdsList.resource.Where(x => x.article_id == AdsList.Id).ToList()
            };
        }

        public IEnumerable<category> GetListCategory()
        {
            return _categoryRepository.Get().ToList();
        }

        public IEnumerable<articleType> GetListSubtypeByCategory(int category_id)
        {
            return _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
        }

        public List<articleType> GetListSubtypeByCategoryAsJson(int category_id)
        {
            var list_types = _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
            var json = new List<articleType>();
            foreach (var obj in list_types) {
                json.Add(new articleType
                { 
                    Id = obj.Id,
                    category_id = obj.category_id,
                    name = obj.name,
                    status = obj.status
                });
            }
            return json;
        }

        public List<moto> GetAllMotoType()
        {
            return _articleRepository.Get().OfType<moto>().ToList();
        }

        public List<auto> GetAllAutoType()
        {
            return _articleRepository.Get().OfType<auto>().ToList();
        }

        public List<article> GetAllArticleTest()
        {
            var AdvertisingLists = _articleRepository.Get().ToList();
            return AdvertisingLists;
        }

        public int CreateMoto(moto moto)
        {
            return _articleRepository.Create(moto);
        }

        public int CreateAuto(auto auto)
        {
            return _articleRepository.Create(auto);
        }
    }
}
