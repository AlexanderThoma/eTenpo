FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# only expose http port (ssl will be done by gateway)
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM base AS base-release
# restrict access to publish folder
RUN adduser -u 6539 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket/Api/Api.csproj", "Services/Basket/Api/"]
RUN dotnet restore "Services/Basket/Api/Api.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base-release AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]

