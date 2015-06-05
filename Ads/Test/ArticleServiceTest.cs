using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ads.Dominio;
using Ads.Repository;
using Ads.Services.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test
{
    [TestFixture]
    public class ArticleServiceTest
    {

        private moto moto = null;
        private auto auto = null;

        private void InitializeTestData()
        {
            var articleRepositoryStub = MockRepository.GenerateMock<IRepository<articles>>();
            var articleTypeRepositoryStub = MockRepository.GenerateMock<IRepository<articleTypes>>();
            var customerRepositoryStub = MockRepository.GenerateMock<IRepository<customers>>();
            var categoryRepositoryStub = MockRepository.GenerateMock<IRepository<categories>>();
            var articleService = new ArticleService(articleRepositoryStub, articleTypeRepositoryStub, customerRepositoryStub, categoryRepositoryStub);

            moto = new moto()
            {
               title = "moto 1",
               detail = "moto 1 detail",
               customer_id = 1,
               category_Id = 1,
               price_moto = 10,
               vin = "001"
            };
            articleService.CreateModel(moto);

            auto = new auto()
            {
                title = "auto 1",
                detail = "auto 1 detail",
                customer_id = 1,
                category_Id = 1,
                price_auto = 50,
                kilometraje = "100 km"
            };
            articleService.CreateModel(auto);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Cliente no encontrado NonUser")]
        public void GetAll_Article()
        {
            InitializeTestData();

            var articleRepositoryStub = MockRepository.GenerateMock<IRepository<articles>>();
            var articleTypeRepositoryStub = MockRepository.GenerateMock<IRepository<articleTypes>>();
            var customerRepositoryStub = MockRepository.GenerateMock<IRepository<customers>>();
            var categoryRepositoryStub = MockRepository.GenerateMock<IRepository<categories>>();
            var articleService = new ArticleService(articleRepositoryStub, articleTypeRepositoryStub, customerRepositoryStub, categoryRepositoryStub);
            List<articles> listIntArticle = articleService.GetAllArticleTest();

            Assert.AreEqual(listIntArticle.Count, 2);
            Assert.IsInstanceOf<moto>(listIntArticle[0]);
            Assert.IsInstanceOf<auto>(listIntArticle[1]);
        }

        [Test]
        public void GetAllMoto_UsingSpecificRepository_Article()
        {
            InitializeTestData();

            var articleRepositoryStub = MockRepository.GenerateMock<IRepository<articles>>();
            var articleTypeRepositoryStub = MockRepository.GenerateMock<IRepository<articleTypes>>();
            var customerRepositoryStub = MockRepository.GenerateMock<IRepository<customers>>();
            var categoryRepositoryStub = MockRepository.GenerateMock<IRepository<categories>>();
            var articleService = new ArticleService(articleRepositoryStub, articleTypeRepositoryStub, customerRepositoryStub, categoryRepositoryStub);
            List<moto> listIntArticle = articleService.GetAllMotoType();

            Assert.AreEqual(listIntArticle.Count, 1);
            Assert.IsInstanceOf<moto>(listIntArticle[0]);
            Assert.AreEqual(listIntArticle[0].title, moto.title);
            Assert.IsNotNull(listIntArticle[0].vin);
            Assert.AreEqual(listIntArticle[0].price_moto, moto.price_moto);
        }
    }
}
