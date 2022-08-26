﻿using Database.Models;
using ProductCatalogue.Models.InputModels;

namespace Database.SpecificationPattern.Specifications
{
    public class SearchProductByGuidSpecification : BaseSpecification<Models.Product>
    {
        public SearchProductByGuidSpecification(ProductSearchInputModel productSearchInputModel)
        {
            Criteria = p => p.Id == productSearchInputModel.Id;
        }
    }
}
