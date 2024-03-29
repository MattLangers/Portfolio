﻿using Database.Models;

namespace Database.Factories
{
    internal interface IDatabaseSeedingFactory
    {
        IList<ProductType> CreateProductTypes();

        IList<Product> CreateProducts();

        IList<ProductDescription> CreateProductDescriptions();

        IList<ProductPrice> CreateProductPrices();

        IList<ProductRanking> CreateProductRankings();
    }
}
