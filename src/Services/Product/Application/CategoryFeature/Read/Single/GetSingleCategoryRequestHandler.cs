using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public class GetSingleCategoryRequestHandler(
    ICategoryRepository repository,
    ILogger<GetSingleCategoryRequestHandler> logger)
    : IQueryHandler<GetSingleCategoryRequest, GetSingleCategoryRequestResponse>
{
    public async Task<GetSingleCategoryRequestResponse> Handle(GetSingleCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get category with id {Id} from database", request.Id);
        var category = await repository.FindById(request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"Category with id {request.Id} could not be found");
        }

        return new GetSingleCategoryRequestResponse(category.Id, category.Name, category.Description);
    }
}