using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Update;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description) : ICommand<UpdateCategoryCommandResponse>
{
    // used for mapping the id in the controller
    public Guid Id { get; set; } = Id;
}