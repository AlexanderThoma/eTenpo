using eTenpo.Basket.Api.Models;
using FluentValidation;

namespace eTenpo.Basket.Api.Validators;

public class BasketItemValidator : AbstractValidator<BasketItem>
{
    public BasketItemValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Basket item id must not be empty");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product id must not be empty");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("Quantity must not be less than 1");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must not be less or equal to zero");
        RuleFor(x => x.OldUnitPrice).GreaterThan(0).WithMessage("Old unit price must not be less or equal to zero");
    }
}
