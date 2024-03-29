﻿using System.Linq.Expressions;
using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Infrastructure.Specifications.Base;

public abstract class BaseSpecification<T>(Expression<Func<T, bool>>? filter)
    where T : AggregateRoot
{
    public bool IsSplitQuery { get; private set; }
    
    public bool AsNoTracking { get; private set; }
    
    public Expression<Func<T, bool>>? FilterCondition { get; } = filter;

    public List<Expression<Func<T, object>>> Includes { get; } = [];
    
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    
    public Expression<Func<T, object>>? GroupBy { get; private set; }
    
    protected void AddInclude(Expression<Func<T, object>> include) => this.Includes.Add(include);

    protected void SetOrderBy(Expression<Func<T, object>> orderBy) => this.OrderBy = orderBy;

    protected void SetOrderByDescending(Expression<Func<T, object>> orderByDescending) =>
        this.OrderByDescending = orderByDescending;
    
    protected void SetGroupBy(Expression<Func<T, object>> groupBy) =>
        this.GroupBy = groupBy;
    
    protected void SetAsNoTracking() =>
        this.AsNoTracking = true;
    
    protected void SetSplitQueryTrue() => this.IsSplitQuery = true;
}