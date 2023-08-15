using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Delete;

public record DeleteCategoryCommand(Guid Id) : ICommand;