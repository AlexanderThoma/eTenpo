namespace eTenpo.Product.Api.Requests.Category;

public record CreateCategoryRequest(string Name, string Description) : CategoryBaseRequest(Name, Description);