﻿using AutoMapper;
using eTenpo.Product.Api.Dtos.Product;
using eTenpo.Product.Application.ProductFeature.Create;
using eTenpo.Product.Application.ProductFeature.Update;

namespace eTenpo.Product.Api.Mapping.ProductFeature;

public class ProductMappingProfile : Profile
{
    // TODO: mapping profiles
    public ProductMappingProfile()
    {
        CreateMap<CreateProductDto, CreateProductCommand>();
        CreateMap<UpdateProductDto, UpdateProductCommand>();
        CreateMap<Domain.AggregateRoots.ProductAggregate.Product, UpdateProductResponse>();
    }
}