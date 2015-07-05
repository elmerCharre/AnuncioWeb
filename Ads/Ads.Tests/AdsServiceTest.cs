using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ads.Dominio;
using Ads.Repository;
using Ads.Services.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Ads.Tests
{
    [TestFixture]
    public class AdsServiceTest
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Articulo guardado")]
        public void create_article_ReturnAnException()
        {
            var _articleRepository = MockRepository.GenerateMock<IRepository<articles>>();
            var _articleTypeRepository = MockRepository.GenerateMock<IRepository<articleTypes>>();
            var _customerRepository = MockRepository.GenerateMock<IRepository<customers>>();
            var _categoryRepository = MockRepository.GenerateMock<IRepository<categories>>();
            var _marcaRepository = MockRepository.GenerateMock<IRepository<marcas>>();
            var _relationMarcaRepository = MockRepository.GenerateMock<IRepository<relationship_marca>>();
            var _conditionRepository = MockRepository.GenerateMock<IRepository<conditions>>();
            var _relationConditionRepository = MockRepository.GenerateMock<IRepository<relationship_condition>>();
            var _modeloRepository = MockRepository.GenerateMock<IRepository<modelos>>();;
            var _tipoRepository = MockRepository.GenerateMock<IRepository<tipos>>();
            var _contactRepository = MockRepository.GenerateMock<IRepository<contacts>>();

            var Service = new ArticleService(_articleRepository, _articleTypeRepository,
            _customerRepository, _categoryRepository, _marcaRepository, _relationMarcaRepository,
            _conditionRepository, _relationConditionRepository, _modeloRepository, _tipoRepository, _contactRepository);
            _articleRepository.Stub(m => m.Get()).Return(new EnumerableQuery<Customer>(new List<Customer>()));
            var playlists = playListService.GetPlayLists("NonUser");
        }
    }
}
