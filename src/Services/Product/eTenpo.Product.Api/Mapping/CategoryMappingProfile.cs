using AutoMapper;
using eTenpo.Product.Api.Requests.Category;
using eTenpo.Product.Application.CategoryFeature.Create;
using eTenpo.Product.Application.CategoryFeature.Read.All;
using eTenpo.Product.Application.CategoryFeature.Read.Single;
using eTenpo.Product.Application.CategoryFeature.Update;
using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

namespace eTenpo.Product.Api.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>()
            .ForMember(command => command.Name,
            expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description));
        
        CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description));
        
        CreateMap<Category, UpdateCategoryCommandResponse>()
            .ForMember(command => command.Id,
                expression => expression.MapFrom(x => x.Id));
        
        CreateMap<Category, GetAllCategoriesRequestResponse>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description));
        
        CreateMap<Category, GetSingleCategoryRequestResponse>()
            .ForMember(command => command.Name,
                expression => expression.MapFrom(x => x.Name))
            .ForMember(command => command.Description,
                expression => expression.MapFrom(x => x.Description));
    }
}