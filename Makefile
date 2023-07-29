# Parameter
MIGRATION_NAME = default

add-migration:
	dotnet ef migrations add $(MIGRATION_NAME) --project ./src/Services/Product/eTenpo.Product.Infrastructure/eTenpo.Product.Infrastructure.csproj --context ApplicationDbContext -s ./src/Services/Product/eTenpo.Product.Api

remove-migration:
	dotnet ef migrations remove --project ./src/Services/Product/eTenpo.Product.Infrastructure/eTenpo.Product.Infrastructure.csproj --context ApplicationDbContext -s ./src/Services/Product/eTenpo.Product.Api	
