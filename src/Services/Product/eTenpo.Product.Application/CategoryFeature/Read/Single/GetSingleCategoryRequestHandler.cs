using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public class GetSingleCategoryRequestHandler : IQueryHandler<GetSingleCategoryRequest, GetSingleCategoryRequestResponse>
{
    private readonly ICategoryRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetSingleCategoryRequestHandler> logger;

    public GetSingleCategoryRequestHandler(ICategoryRepository repository, IMapper mapper, ILogger<GetSingleCategoryRequestHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<GetSingleCategoryRequestResponse> Handle(GetSingleCategoryRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get category with id {Id} from database", request.Id);
        var product = await this.repository.FindById(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }
        
        return this.mapper.Map<GetSingleCategoryRequestResponse>(product);
    }
}