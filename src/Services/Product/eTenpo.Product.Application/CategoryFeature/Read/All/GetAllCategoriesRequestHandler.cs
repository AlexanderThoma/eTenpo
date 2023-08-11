using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Read.All;

public class GetAllCategoriesRequestHandler : IQueryHandler<GetAllCategoriesRequest, List<GetAllCategoriesRequestResponse>>
{
    private readonly ICategoryRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetAllCategoriesRequestHandler> logger;

    public GetAllCategoriesRequestHandler(ICategoryRepository repository, IMapper mapper, ILogger<GetAllCategoriesRequestHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<List<GetAllCategoriesRequestResponse>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get all products with category from database");
        
        var products = await this.repository.GetAll(cancellationToken);

        return this.mapper.Map<List<GetAllCategoriesRequestResponse>>(products);
    }
}