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
        private IRepository<articles> _articleRepository;
        private IRepository<articleTypes> _articleTypeRepository;
        private IRepository<customers> _customerRepository;
        private IRepository<categories> _categoryRepository;

        public ArticleService(IRepository<articles> articleRepository,
            IRepository<articleTypes> articleTypeRepository, 
            IRepository<customers> customerRepository, IRepository<categories> categoryRepository)
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
            var articles = _articleRepository.Get().OfType<camion>().ToList();
            //var articles = _articleRepository.Get().ToList();
            return articles.Select(AdsList => new ArticleViewModel(AdsList)).ToList();
        }
         
        public void Dispose()
        {
            _articleRepository = null;
            _customerRepository = null;
        }

        public int Create(Object modelView)
        {
            //var model = modelView;
            //if (modelView is MotoViewModel)
            //{
                MotoViewModel model = (MotoViewModel) modelView;
                var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
                if (customer == null) throw new InvalidOperationException("Cliente no encontrado");

                var Entity = new moto()
                {
                    title = model.title,
                    detail = model.detail,
                    customer_id = 1,
                    category_Id = model.category_Id,
                    precio = model.precio,
                    marca = model.marca,
                    modelo = model.modelo,
                    anio = model.anio,
                    vin = model.vin,
                    condicion = model.condicion,
                    kilometraje = model.kilometraje
                };
                return _articleRepository.Create(Entity);
            //}

            //return 1;

            //var advertising = new article()
            //{
            //    title = model.title,
            //    detail = model.detail,
            //    customer_id = customer.Id,
            //    //articleType = model.articleType,
            //    resource = model.resource
            //};
            //return _articleRepository.Create(advertising);
            return 1;
        }

        public void Update(ArticleViewModel model)
        {
            var customer = _customerRepository.Get().FirstOrDefault(x => x.Id == model.customer_id);
            if (customer == null) throw new InvalidOperationException("Cliente no encontrado");
            //var advertising = new article()
            //{
            //    title = model.title,
            //    detail = model.detail,
            //    customer_id = customer.Id,
            //    //articleType = model.articleType,
            //    resource = model.resource
            //};
            //_articleRepository.Update(advertising);
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
                resource = AdsList.resources.Where(x => x.article_id == AdsList.Id).ToList()
            };
        }

        public IEnumerable<categories> GetListCategory()
        {
            return _categoryRepository.Get().ToList();
        }

        public IEnumerable<articleTypes> GetListSubtypeByCategory(int category_id)
        {
            return _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
        }

        public List<articleTypes> GetListSubtypeByCategoryAsJson(int category_id)
        {
            var list_types = _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
            var json = new List<articleTypes>();
            foreach (var obj in list_types) {
                json.Add(new articleTypes
                { 
                    Id = obj.Id,
                    category_id = obj.category_id,
                    name = obj.name,
                    type = obj.type
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

        public List<articles> GetAllArticleTest()
        {
            var AdvertisingLists = _articleRepository.Get().ToList();
            return AdvertisingLists;
        }

        public int CreateModel(articles entity)
        {
            return _articleRepository.Create(entity);
        }
    }
}
