using AutoMapper;
using eTenpo.Product.Api.Requests.Product;
using eTenpo.Product.Application.ProductFeature.Create;
using eTenpo.Product.Application.ProductFeature.Read.All;
using eTenpo.Product.Application.ProductFeature.Read.Single;
using eTenpo.Product.Application.ProductFeature.Update;

namespace eTenpo.Product.Api.Mapping;

/// <summary>
/// This class contains all product related mappings
/// To prevent bugs by adding, modifying or removing fields, all fields are included here
/// </summary>
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(command => command.Name,
            expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description))
            .ForMember(command => command.AvailableStock,
                expression => expression.MapFrom(x => x.AvailableStock))
            .ForMember(command => command.CategoryId,
                expression => expression.MapFrom(x => x.CategoryId));
        
        CreateMap<Domain.AggregateRoots.ProductAggregate.Product, CreateProductCommandResponse>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description))
            .ForMember(command => command.AvailableStock,
                expression => expression.MapFrom(x => x.AvailableStock))
            .ForMember(command => command.CategoryId,
                expression => expression.MapFrom(x => x.CategoryId));
        
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description))
            .ForMember(command => command.CategoryId,
                expression => expression.MapFrom(x => x.CategoryId));
        
        CreateMap<Domain.AggregateRoots.ProductAggregate.Product, UpdateProductCommandResponse>()
            .ForMember(command => command.Id,
                expression => expression.MapFrom(x => x.Id));
        
        CreateMap<Domain.AggregateRoots.ProductAggregate.Product, GetAllProductsRequestResponse>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description))
            .ForMember(command => command.AvailableStock,
                expression => expression.MapFrom(x => x.AvailableStock))
            .ForMember(command => command.CategoryId,
                expression => expression.MapFrom(x => x.CategoryId));
        
        CreateMap<Domain.AggregateRoots.ProductAggregate.Product, GetSingleProductRequestResponse>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Price,
                expression => expression.MapFrom(x => x.Price))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description))
            .ForMember(command => command.AvailableStock,
                expression => expression.MapFrom(x => x.AvailableStock))
            .ForMember(command => command.CategoryId,
                expression => expression.MapFrom(x => x.CategoryId));
    }
}