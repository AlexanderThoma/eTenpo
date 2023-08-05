using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Delete;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    public DeleteCategoryCommandHandler()
    {
        
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}