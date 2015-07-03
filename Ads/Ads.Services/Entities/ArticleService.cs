using System.Linq;
using Ads.Dominio;
using Ads.Repository;
using Ads.Common.ViewModels;
using System.Collections.Generic;
using System;
using System.Web;
using AutoMapper;

namespace Ads.Services.Entities
{
    public class ArticleService
    {
        private IRepository<articles> _articleRepository;
        private IRepository<articleTypes> _articleTypeRepository;
        private IRepository<customers> _customerRepository;
        private IRepository<categories> _categoryRepository;
        private IRepository<marcas> _marcaRepository;
        private IRepository<relationship_marca> _relationMarcaRepository;
        private IRepository<conditions> _conditionRepository;
        private IRepository<relationship_condition> _relationConditionRepository;
        private IRepository<modelos> _modeloRepository;
        private IRepository<tipos> _tipoRepository;

        public ArticleService(IRepository<articles> articleRepository,
            IRepository<articleTypes> articleTypeRepository, 
            IRepository<customers> customerRepository, IRepository<categories> categoryRepository,
            IRepository<marcas> marcaRepository, IRepository<relationship_marca> relationMarcaRepository,
            IRepository<conditions> conditionRepository, IRepository<relationship_condition> relationConditionRepository,
            IRepository<modelos> modeloRepository, IRepository<tipos> tipoRepository)
        {
            _articleRepository = articleRepository;
            _articleTypeRepository = articleTypeRepository;
            _customerRepository = customerRepository;
            _categoryRepository = categoryRepository;
            _marcaRepository = marcaRepository;
            _relationMarcaRepository = relationMarcaRepository;
            _conditionRepository = conditionRepository;
            _relationConditionRepository = relationConditionRepository;
            _modeloRepository = modeloRepository;
            _tipoRepository = tipoRepository;
        }

        public IEnumerable<ArticleViewModel> GetListByUser(string userName)
        {
            var customer = _customerRepository.Get().FirstOrDefault(c => c.email == userName);
            if (customer == null) throw new InvalidOperationException(string.Format("Cliente no encontrado {0}", userName));
            var AdvertisingLists = _articleRepository.Get().Where(x => x.customer_id == customer.Id).ToList();
            return AdvertisingLists.Select(AdsList => new ArticleViewModel(AdsList)).ToList();
        }

        public AutoViewModel getAuto(int id)
        {
            var article = _articleRepository.Get().OfType<auto>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<auto, auto>();
            var entity = Mapper.Map<auto>(article);
            var model = new AutoViewModel(entity);
            model.marca_name = _marcaRepository.Get().FirstOrDefault(x => x.Id == model.marca).name;
            model.modelo_name = _modeloRepository.Get().FirstOrDefault(x => x.Id == model.modelo).name;
            model.tipo_name = _tipoRepository.Get().FirstOrDefault(x => x.Id == model.tipo).name;
            model.condicion_name = _conditionRepository.Get().FirstOrDefault(x => x.Id == model.condicion).name;
            return model;
        }

        public IEnumerable<CategoryViewModel> getListCategories()
        {
            var categories = _categoryRepository.Get().OrderBy(c => c.status).ToList();
            return categories.Select(category => new CategoryViewModel(category)).ToList();
        }
        
        public void Dispose()
        {
            _articleRepository = null;
            _customerRepository = null;
        }

        public int Create(articles Entity)
        {
            return _articleRepository.Create(Entity);
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
            var article = _articleRepository.Get().FirstOrDefault(x => x.Id == id);
            if (article == null) throw new InvalidOperationException("Anuncio no encontrado");
            return new ArticleViewModel(article);
        }

        public IEnumerable<categories> GetListCategory()
        {
            return _categoryRepository.Get().ToList();
        }

        public IEnumerable<MarcaViewModel> GetListMarca(int articleType_id)
        {
            var rel_marcas = _relationMarcaRepository.Get().Where(r => r.articleType_id == articleType_id).ToList();
            var json = new List<MarcaViewModel>();
            foreach (var rel_marca in rel_marcas)
            {
                var marca = _marcaRepository.Get().FirstOrDefault(m => m.Id == rel_marca.marca_id);
                json.Add(new MarcaViewModel {
                    Id = marca.Id,
                    Name = marca.name
                });
            }
            return json;
        }

        public IEnumerable<ConditionViewModel> GetListCondition(int articleType_id)
        {
            var rel_condiciones = _relationConditionRepository.Get().Where(r => r.articleType_id == articleType_id).ToList();
            var json = new List<ConditionViewModel>();
            foreach (var rel_condicion in rel_condiciones)
            {
                var condicion = _conditionRepository.Get().FirstOrDefault(m => m.Id == rel_condicion.condition_id);
                json.Add(new ConditionViewModel
                {
                    Id = condicion.Id,
                    Name = condicion.name
                });
            }
            return json;
        }

        public IEnumerable<TipoViewModel> GetListTipo(int articleType_id)
        {
            var tipos = _tipoRepository.Get().Where(r => r.articleType_id == articleType_id).ToList();
            return tipos.Select(tipo => new TipoViewModel(tipo)).ToList();
        }

        public IEnumerable<articleTypes> GetListSubtypeByCategory(int category_id)
        {
            return _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
        }

        public IEnumerable<ArticleTypeViewModel> GetListSubtypeByCategoryAsJson(int category_id)
        {
            var article_type = _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
            return article_type.Select(type => new ArticleTypeViewModel(type)).ToList();
        }

        public IEnumerable<ModeloViewModel> GetListModeloByMarca(int marca_id)
        {
            var modelos = _modeloRepository.Get().Where(x => x.marca_id == marca_id).ToList();
            return modelos.Select(modelo => new ModeloViewModel(modelo)).ToList();
        }

        public IEnumerable<ArticleViewModel> getArticlesByCategory(int category_id)
        {
            var articles = _articleRepository.Get().Where(x => x.category_Id == category_id).ToList();
            return articles.Select(article => new ArticleViewModel(article)).ToList();
        }

        public IEnumerable<ArticleTypeViewModel> GetListArticleTypeByCategory(int category_id)
        {
            var article_type = _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
            return article_type.Select(type => new ArticleTypeViewModel(type)).ToList();
        }

        public int deleteArticle(int article_id)
        {
            var article = _articleRepository.Get().FirstOrDefault(x => x.Id == article_id);
            return _articleRepository.Delete(article);
        }

        public int CreateModel(articles entity)
        {
            return _articleRepository.Create(entity);
        }
    }
}
