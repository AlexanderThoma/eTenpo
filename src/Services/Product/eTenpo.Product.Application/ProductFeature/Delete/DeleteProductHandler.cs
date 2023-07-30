using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.FindById(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }
        
        // mark as deleted in changeTracker
        this.productRepository.Delete(product);
        
        // generate domain event
        product.Delete();
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);
    }
}