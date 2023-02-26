FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Product/eTenpo.Product.Api/eTenpo.Product.Api.csproj", "Services/Product/eTenpo.Product.Api/"]
RUN dotnet restore "Services/Product/eTenpo.Product.Api/eTenpo.Product.Api.csproj"
COPY . .
WORKDIR "/src/Services/Product/eTenpo.Product.Api"
RUN dotnet build "eTenpo.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eTenpo.Product.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eTenpo.Product.Api.dll"]
