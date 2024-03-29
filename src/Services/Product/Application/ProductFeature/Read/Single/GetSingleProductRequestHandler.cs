﻿using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Read.Single;

public class GetSingleProductRequestHandler : IQueryHandler<GetSingleProductRequest, GetSingleProductRequestResponse>
{
    private readonly IProductRepository repo;
    private readonly ILogger<GetSingleProductRequestHandler> logger;

    public GetSingleProductRequestHandler(IProductRepository repo, ILogger<GetSingleProductRequestHandler> logger)
    {
        this.repo = repo;
        this.logger = logger;
    }
    
    public async Task<GetSingleProductRequestResponse> Handle(GetSingleProductRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get product with id {Id} including category from database", request.Id);
        var product = await this.repo.FindByIdWithCategory(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }

        return new GetSingleProductRequestResponse(product.Id, product.ProductName, product.Price,
            product.ProductDescription, product.AvailableStock, product.CategoryId);
    }
}