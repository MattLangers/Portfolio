﻿using API.Models.InputModels;
using Database.Factories;
using Database.Models;
using Database.Models.DTO;
using Database.Search;
using Database.SpecificationPattern.Specifications;
using Moq;
using Moq.AutoMock;

namespace Database.Tests.Search.DatabaseSearchOrchestrator.SearchProducts
{
    public sealed class DatabaseSearchOrchestrator_SearchProducts_ProductType_With_Match_No_Tests
    {
        private const int ProductTypeId = 111;

        private static readonly Models.ProductType productType = new Models.ProductType() { Name = "Name", Id = ProductTypeId };

        private static readonly Models.Product product = new() { Name = "Name", ProductType = productType };

        private readonly ProductSearchInputModel productSearchInputModel = new ProductSearchInputModel() { ProductTypeId = 222 };

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

            autoMocker.Use<ISearchProductSpecificationFactory>(m => m.CreateSearchByProductType(productSearchInputModel) == new SearchProductByProductTypeSpecification(productSearchInputModel));

            autoMocker.Use<IMapProductsToDto>(m => m.Map(It.Is<IQueryable<Models.Product>>(m => !m.Any())) == expectedResult);

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
        public void Verify_CreateSearchByName_is_not_called()
        {
            autoMocker.Verify<ISearchProductSpecificationFactory>(m => m.CreateSearchByName(It.IsAny<ProductSearchInputModel>()), Times.Never);
        }

        [Test]
        public void Verify_CreateSearchForAllProducts_is_not_called()
        {
            autoMocker.Verify<ISearchProductSpecificationFactory>(m => m.CreateSearchForAllProducts(), Times.Never);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            databaseContext.Dispose();
        }
    }
}
