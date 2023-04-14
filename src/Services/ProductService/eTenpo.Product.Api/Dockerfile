FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# only expose http port (ssl will be done by gateway)
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM base AS base-release
# restrict access to publish folder
RUN adduser -u 6539 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Product/eTenpo.Product.Api/eTenpo.Product.Api.csproj", "Services/Product/eTenpo.Product.Api/"]
RUN dotnet restore "Services/Product/eTenpo.Product.Api/eTenpo.Product.Api.csproj"
COPY . .
WORKDIR "/src/Services/Product/eTenpo.Product.Api"
RUN dotnet build "eTenpo.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
# /p -> prevent generation of executable
RUN dotnet publish "eTenpo.Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base-release AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eTenpo.Product.Api.dll"]
