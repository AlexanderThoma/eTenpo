namespace eTenpo.Product.Api.Requests.Category;

public record UpdateCategoryRequest(string Name, string Description) : CategoryBaseRequest(Name, Description);