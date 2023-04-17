using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IProductRepository repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // create in repo
        var product = new Domain.AggregateRoots.ProductAggregate.Product(
            new Name(request.Name),
            new Price(request.Price),
            new Description(request.Description),
            new CategoryId(request.CategoryId));
        
        
        
        return new CreateProductCommandResponse(Guid.NewGuid());
    }
}