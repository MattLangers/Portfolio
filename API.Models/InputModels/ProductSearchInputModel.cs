﻿namespace API.Models.InputModels
{
    public sealed class ProductSearchInputModel
    {
        public Guid? Id { get; init; }

        public int ProductTypeId { get; init; }

        public string? Name { get; init; }

        public bool Archived { get; set; } = false;

        public bool HasSearchPropertiesDefined { get; init; } = false;
    }
}
