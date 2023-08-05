using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<DeleteProductCommandHandler> logger;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<DeleteProductCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get the product with id {Id} from database", request.Id);
        
        var product = await this.productRepository.FindById(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }
        
        this.logger.LogInformation("Mark the product as deleted");
        this.productRepository.Delete(product);
        
        this.logger.LogInformation("Create the productDeleted event");
        product.Delete();
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);
    }
}