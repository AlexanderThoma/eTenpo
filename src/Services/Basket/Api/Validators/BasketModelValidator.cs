using eTenpo.Basket.Api.Models;
using eTenpo.Basket.Api.Services;
using FluentValidation;

namespace eTenpo.Basket.Api.Validators;

public class BasketModelValidator : AbstractValidator<BasketModel>
{
    public BasketModelValidator(IBasketRepository basketRepository)
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer id must not be empty");
        RuleFor(x => x.CustomerId)
            .MustAsync(async (id, _) => (await basketRepository.GetBasket(id)) is null)
            .WithMessage("Another basket for this user already exists");
        RuleFor(x => x.Items).Must(HasNoDuplicates).WithMessage("Basket contains duplicate products");
        RuleFor(x => x.Items).NotNull().WithMessage("BasketItems must not be null");
        RuleForEach(x => x.Items).SetValidator(new BasketItemValidator());
    }

    private static bool HasNoDuplicates(List<BasketItem> items)
    {
        return !items
            .GroupBy(y => y.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .Any();
    }
}
