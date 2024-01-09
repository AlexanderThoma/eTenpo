using System.Text.Json.Serialization;

namespace eTenpo.Basket.Api.Models;

// seems to have a bug: https://github.com/dotnet/runtime/issues/92877
// enable to populate read-only properties
//[JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
public class BasketModel
{
    public Guid CustomerId { get; init; }

    public List<BasketItem> Items { get; init; } = [];
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true)]
[JsonSerializable(typeof(BasketModel))]
internal partial class BasketModelContext : JsonSerializerContext;
