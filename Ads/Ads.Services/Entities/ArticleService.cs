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
    public class ArticleService : IDisposable
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
        private IRepository<contacts> _contactRepository;

        public ArticleService(IRepository<articles> articleRepository,
            IRepository<articleTypes> articleTypeRepository, 
            IRepository<customers> customerRepository, IRepository<categories> categoryRepository,
            IRepository<marcas> marcaRepository, IRepository<relationship_marca> relationMarcaRepository,
            IRepository<conditions> conditionRepository, IRepository<relationship_condition> relationConditionRepository,
            IRepository<modelos> modeloRepository, IRepository<tipos> tipoRepository, IRepository<contacts> contactRepository)
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
            _contactRepository = contactRepository;
        }

        public int CreateContact(ContactViewModel model)
        {
            Mapper.CreateMap<ContactViewModel, contacts>();
            var entity = Mapper.Map<ContactViewModel, contacts>(model);
            return _contactRepository.Create(entity);
        }

        /* Methods for Basic Model */
        public ArticleViewModel getModel(int id)
        {
            var article = _articleRepository.Get().OfType<model>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<model, model>();
            var entity = Mapper.Map<model>(article);
            return new ArticleViewModel(entity);
        }

        public IEnumerable<ArticleViewModel> getModels(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<model>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new ArticleViewModel(a)).ToList();
            return articles;
        }

        /* Methods for Auto Model */
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

        public IEnumerable<AutoViewModel> getAutos(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<auto>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new AutoViewModel(a)).ToList();
            return articles;
        }

        /* Methods for Moto Model */
        public MotoViewModel getMoto(int id)
        {
            var article = _articleRepository.Get().OfType<moto>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<moto, moto>();
            var entity = Mapper.Map<moto>(article);
            var model = new MotoViewModel(entity);
            model.marca_name = _marcaRepository.Get().FirstOrDefault(x => x.Id == model.marca).name;
            model.modelo_name = _modeloRepository.Get().FirstOrDefault(x => x.Id == model.modelo).name;
            model.condicion_name = _conditionRepository.Get().FirstOrDefault(x => x.Id == model.condicion).name;
            return model;
        }

        public IEnumerable<MotoViewModel> getMotos(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<moto>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new MotoViewModel(a)).ToList();
            return articles;
        }

        /* Methods for Camion Model */
        public CamionViewModel getCamion(int id)
        {
            var article = _articleRepository.Get().OfType<camion>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<camion, camion>();
            var entity = Mapper.Map<camion>(article);
            var model = new CamionViewModel(entity);
            model.marca_name = _marcaRepository.Get().FirstOrDefault(x => x.Id == model.marca).name;
            model.condicion_name = _conditionRepository.Get().FirstOrDefault(x => x.Id == model.condicion).name;
            return model;
        }

        public IEnumerable<CamionViewModel> getCamiones(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<camion>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new CamionViewModel(a)).ToList();
            return articles;
        }

        /* Methods for Depa Alquiler Model */
        public DepartamentoAlquilerViewModel getDepaAlquiler(int id)
        {
            var article = _articleRepository.Get().OfType<depa_alquiler>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<depa_alquiler, depa_alquiler>();
            var entity = Mapper.Map<depa_alquiler>(article);
            var model = new DepartamentoAlquilerViewModel(entity);
            model.amueblado_name = _conditionRepository.Get().FirstOrDefault(x => x.Id == model.amueblado).name;
            model.comision_name = _conditionRepository.Get().FirstOrDefault(x => x.Id == model.comision).name;
            return model;
        }

        public IEnumerable<DepartamentoAlquilerViewModel> getDepaAlquileres(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<depa_alquiler>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new DepartamentoAlquilerViewModel(a)).ToList();
            return articles;
        }

        /* Methods for Empleo Oferta Model */
        public OfertaEmpleoViewModel getEmpleoOferta(int id)
        {
            var article = _articleRepository.Get().OfType<oferta>().FirstOrDefault(x => x.Id == id);
            Mapper.CreateMap<oferta, oferta>();
            var entity = Mapper.Map<oferta>(article);
            var model = new OfertaEmpleoViewModel(entity);
            model.tipo_name = _tipoRepository.Get().FirstOrDefault(x => x.Id == model.tipo).name;
            return model;
        }

        public IEnumerable<OfertaEmpleoViewModel> getEmpleoOfertas(int limit = 10)
        {
            var articles = _articleRepository.Get().OfType<oferta>().OrderByDescending(x => x.Id).Take(limit).Select(
                a => new OfertaEmpleoViewModel(a)).ToList();
            return articles;
        }

        public IEnumerable<CategoryViewModel> getListCategories()
        {
            var categories = _categoryRepository.Get().OrderBy(c => c.status).Select(
                c => new CategoryViewModel(c)).ToList();
            return categories;
        }
        
        public void Dispose()
        {
            _articleRepository = null;
            _customerRepository = null;
            _articleTypeRepository = null;
            _categoryRepository = null;
            _conditionRepository = null;
            _contactRepository = null;
            _marcaRepository = null;
            _modeloRepository = null;
            _relationConditionRepository = null;
            _relationMarcaRepository = null;
            _tipoRepository = null;
        }

        public int Create(articles Entity)
        {
            return _articleRepository.Create(Entity);
        }

        public void Update(articles Entity)
        {
            _articleRepository.Update(Entity);
        }

        public int getRows()
        {
            return _articleRepository.Get().Count();
        }

        public IEnumerable<ArticleViewModel> getArticles(int page_number)
        {
            var articles = _articleRepository.Get().OrderByDescending(x => x.Id).Skip(page_number).Take(10).Select(
                a => new ArticleViewModel(a)).ToList();
            return articles;
        }

        public IEnumerable<ArticleViewModel> getArticlesSearch(string text)
        {
            var articles = _articleRepository.Get().OrderByDescending(x => x.Id).Where(x => (x.title.Contains(text) || x.detail.Contains(text))).Select(
                a => new ArticleViewModel(a)).ToList();
            return articles;
        }

        public ArticleViewModel Get(int id)
        {
            var article = _articleRepository.Get().FirstOrDefault(x => x.Id == id);
            return new ArticleViewModel(article);
        }

        public IEnumerable<categories> GetListCategory()
        {
            return _categoryRepository.Get().ToList();
        }

        public IEnumerable<MarcaViewModel> GetListMarca(int articleType_id)
        {
            var marcas = _relationMarcaRepository.Get().Where(r => r.articleType_id == articleType_id).Select(
                m => new MarcaViewModel {
                    Id = m.Id,
                    Name = m.marcas.name
                }).ToList();
            return marcas;
        }

        public IEnumerable<ConditionViewModel> GetListCondition(int articleType_id)
        {
            var condiciones = _relationConditionRepository.Get().Where(r => r.articleType_id == articleType_id).Select(
                c => new ConditionViewModel
                {
                    Id = c.Id,
                    Name = c.conditions.name
                }).ToList();
            return condiciones;
        }

        public IEnumerable<TipoViewModel> GetListTipo(int articleType_id)
        {
            var tipos = _tipoRepository.Get().Where(r => r.articleType_id == articleType_id).Select(
                t => new TipoViewModel(t)).ToList();
            return tipos;
        }

        public IEnumerable<articleTypes> GetListSubtypeByCategory(int category_id)
        {
            return _articleTypeRepository.Get().Where(x => x.category_id == category_id).ToList();
        }

        public IEnumerable<ArticleTypeViewModel> GetListSubtypeByCategoryAsJson(int category_id)
        {
            var types = _articleTypeRepository.Get().Where(x => x.category_id == category_id).Select(
                t => new ArticleTypeViewModel(t)).ToList();
            return types;
        }

        public IEnumerable<ModeloViewModel> GetListModeloByMarca(int marca_id)
        {
            var modelos = _modeloRepository.Get().Where(x => x.marca_id == marca_id).Select(
                m => new ModeloViewModel(m)).ToList();
            return modelos;
        }

        public IEnumerable<ArticleViewModel> getArticlesByCategory(int category_id)
        {
            var articles = _articleRepository.Get().Where(x => x.category_Id == category_id).Select(
                a => new ArticleViewModel(a)).ToList();
            return articles;
        }

        public IEnumerable<ArticleTypeViewModel> GetListArticleTypeByCategory(int category_id)
        {
            var types = _articleTypeRepository.Get().Where(x => x.category_id == category_id).Select(
                t => new ArticleTypeViewModel(t)).ToList();
            return types;
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

        public IEnumerable<ContactViewModel> getMessages(int article_id)
        {
            var messages = _contactRepository.Get().Where(r => r.article_id == article_id).Select(
                t => new ContactViewModel(t)).ToList();
            return messages;
        }
    }
}
