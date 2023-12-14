# Parameter
MIGRATION_NAME = default

add-migration:
	dotnet ef migrations add $(MIGRATION_NAME) --project ./src/Services/Product/Infrastructure/Infrastructure.csproj --context ApplicationDbContext --startup-project ./src/Services/Product/Api

remove-migration:
	dotnet ef migrations remove --project ./src/Services/Product/Infrastructure/Infrastructure.csproj --context ApplicationDbContext --startup-project ./src/Services/Product/Api	
