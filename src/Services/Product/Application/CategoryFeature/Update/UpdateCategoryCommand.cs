using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Update;

public record UpdateCategoryCommand(Guid Id, string Name, string Description) : ICommand<UpdateCategoryCommandResponse>;