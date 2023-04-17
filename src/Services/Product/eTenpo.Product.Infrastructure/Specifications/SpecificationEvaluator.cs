using eTenpo.Product.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Specifications;

/// <summary>
/// Naive specification evaluator (does not include all EF core functionality, e.g. thenInclude)
/// </summary>

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQueryable,
        BaseSpecification<T> specification)
        where T : AggregateRoot
    {
        if (specification.FilterCondition is not null)
        {
            inputQueryable = inputQueryable.Where(specification.FilterCondition);
        }

        if (specification.Includes.Any())
        {
            inputQueryable = specification.Includes.Aggregate(inputQueryable, (current, includeExpression) => current.Include(includeExpression));    
        }

        if (specification.OrderBy is not null)
        {
            inputQueryable = inputQueryable.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            inputQueryable = inputQueryable.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            inputQueryable = inputQueryable.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsSplitQuery)
        {
            inputQueryable = inputQueryable.AsSplitQuery();
        }
        
        return inputQueryable;
    }
}