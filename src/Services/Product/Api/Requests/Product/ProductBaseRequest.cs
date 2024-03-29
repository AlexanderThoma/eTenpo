﻿namespace eTenpo.Product.Api.Requests.Product;

public abstract record ProductBaseRequest(
    string Name,
    decimal Price,
    string Description,
    Guid CategoryId);