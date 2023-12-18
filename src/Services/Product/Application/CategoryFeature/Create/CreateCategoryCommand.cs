using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Create;

public record CreateCategoryCommand(
    string Name,
    string Description) : ICommand<CreateCategoryCommandResponse>;