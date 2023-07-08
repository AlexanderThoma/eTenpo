using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IProductRepository repository;
    private readonly ICategoryRepository categoryRepository;

    public CreateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        this.repository = repository;
        this.categoryRepository = categoryRepository;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // validate category id
        if (!await this.categoryRepository.Exists(request.CategoryId))
        {
            throw new ProductValidationException($"Category with Id \"{request.CategoryId}\" does not exist",
                new ArgumentException(null, nameof(request.CategoryId)));
        }
        
        // create in repo
        var product = new Domain.AggregateRoots.ProductAggregate.Product(
            new Name(request.Name),
            new Price(request.Price),
            new Description(request.Description),
            new CategoryId(request.CategoryId));

        var id = await this.repository.Add(product);
        
        // createdEvent called in ctor of aggregate

        return new CreateProductCommandResponse(id);
    }
}