using eTenpo.Product.Application.CommandQueryAbstractions;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read.Single;

public record GetSingleProductRequest(Guid Id) : IQuery<GetSingleProductRequestResponse>;