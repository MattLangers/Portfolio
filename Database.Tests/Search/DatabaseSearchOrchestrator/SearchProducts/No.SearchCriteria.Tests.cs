﻿using API.Models.InputModels;
using Database.Factories;
using Database.Models;
using Database.Models.DTO;
using Database.Search;
using Database.SpecificationPattern.Specifications.Product;
using Moq;
using Moq.AutoMock;

namespace Database.Tests.Search.DatabaseSearchOrchestrator.SearchProducts
{
    public sealed class DatabaseSearchOrchestrator_SearchProducts_No_SearchCriteria_Tests
    {
        private static readonly Models.Product product = new() { Name = "Name" };

        private readonly ProductSearchInputModel productSearchInputModel = new ProductSearchInputModel();

        private readonly IList<ProductDto> expectedResult = new List<ProductDto>();

        private readonly AutoMocker autoMocker = new AutoMocker();

        private readonly InMemoryDatabaseContextFactory databaseContextFactory = new InMemoryDatabaseContextFactory();

        private DatabaseContext databaseContext;

        private IList<ProductDto> result;

        [OneTimeSetUp]
        public void Setup()
        {
            databaseContextFactory.WithProduct(product);

            databaseContext = databaseContextFactory.GetContext();

            autoMocker.Use(databaseContext);

            autoMocker.Use<ISearchProductSpecificationFactory>(m => m.CreateSearchForAllProducts() == new SearchAllProductSpecification());

            autoMocker.Use<IMapProductsToDto>(m => m.Map(It.Is<IQueryable<Models.Product>>(m => m.Contains(product))) == expectedResult);

            result = autoMocker.CreateInstance<Database.Search.DatabaseSearchOrchestrator>().SearchProducts(productSearchInputModel);
        }

        [Test]
        public void Result_is_expected_object()
        {
            Assert.That(result, Is.SameAs(expectedResult));
        }

        [Test]
        public void Verify_All()
        {
            autoMocker.VerifyAll();
        }

        [Test]
        public void Verify_CreateSearchByGuid_is_not_called()
        {
            autoMocker.Verify<ISearchProductSpecificationFactory>(m => m.CreateSearchByGuid(It.IsAny<ProductSearchInputModel>()), Times.Never);
        }

        [Test]
        public void Verify_CreateSearchByProductType_is_not_called()
        {
            autoMocker.Verify<ISearchProductSpecificationFactory>(m => m.CreateSearchByProductType(It.IsAny<ProductSearchInputModel>()), Times.Never);
        }

        [Test]
        public void Verify_CreateSearchByName_is_not_called()
        {
            autoMocker.Verify<ISearchProductSpecificationFactory>(m => m.CreateSearchByName(It.IsAny<ProductSearchInputModel>()), Times.Never);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            databaseContext.Dispose();
        }
    }
}
