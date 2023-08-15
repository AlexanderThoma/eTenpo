using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public class GetSingleCategoryRequestHandler : IQueryHandler<GetSingleCategoryRequest, GetSingleCategoryRequestResponse>
{
    private readonly ICategoryRepository repository;
    private readonly ILogger<GetSingleCategoryRequestHandler> logger;

    public GetSingleCategoryRequestHandler(ICategoryRepository repository, ILogger<GetSingleCategoryRequestHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }
    
    public async Task<GetSingleCategoryRequestResponse> Handle(GetSingleCategoryRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get category with id {Id} from database", request.Id);
        var category = await this.repository.FindById(request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"Category with id {request.Id} could not be found");
        }

        return new GetSingleCategoryRequestResponse(category.Id, category.Name, category.Description);
    }
}