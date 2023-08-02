using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Read.Single;

public class GetSingleProductHandler : IQueryHandler<GetSingleProductRequest, GetSingleProductResponse>
{
    private readonly IProductRepository repo;
    private readonly IMapper mapper;
    private readonly ILogger<GetSingleProductHandler> logger;

    public GetSingleProductHandler(IProductRepository repo, IMapper mapper, ILogger<GetSingleProductHandler> logger)
    {
        this.repo = repo;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<GetSingleProductResponse> Handle(GetSingleProductRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get product with id {Id} including category from database", request.Id);
        var product = await this.repo.FindByIdWithCategory(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }
        
        return this.mapper.Map<GetSingleProductResponse>(product);
    }
}