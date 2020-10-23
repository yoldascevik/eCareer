FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR ..
COPY ["src/Services/Definition/Definition.Api/Definition.Api.csproj", "src/Services/Definition/Definition.Api/"]
COPY ["src/Services/Definition/Definition.Application/Definition.Application.csproj", "src/Services/Definition/Definition.Application/"]
COPY ["src/Services/Definition/Definition.Data/Definition.Data.csproj", "src/Services/Definition/Definition.Data/"]
COPY ["src/Framework/Career.Shared/Career.Shared.csproj", "src/Framework/Career.Shared/"]
COPY ["src/Framework/Career.Exceptions/Career.Exceptions.csproj", "src/Framework/Career.Exceptions/"]
COPY ["src/Framework/Career.Migration/Career.Migration.csproj", "src/Framework/Career.Migration/"]
COPY ["src/Framework/Career.Repositories/Career.Repositories.csproj", "src/Framework/Career.Repositories/"]
COPY ["src/Framework/Career.IoC/Career.IoC.csproj", "src/Framework/Career.IoC/"]
COPY ["src/Framework/Career.Mongo/Career.Mongo.csproj", "src/Framework/Career.Mongo/"]
COPY ["src/Framework/Career.Mvc/Career.Mvc.csproj", "src/Framework/Career.Mvc/"]
COPY ["src/Framework/Career.Swagger/Career.Swagger.csproj", "src/Framework/Career.Swagger/"]
COPY ["src/Framework/Career.Data/Career.Data.csproj", "src/Framework/Career.Data/"]
COPY ["src/Framework/Career.Logging/Career.Logging.csproj", "src/Framework/Career.Logging/"]
COPY ["src/Framework/Career.Cache/Career.Cache.csproj", "src/Framework/Career.Cache/"]
COPY ["src/Framework/Career.Cache.Redis/Career.Cache.Redis.csproj", "src/Framework/Career.Cache.Redis/"]
RUN dotnet restore "src/Services/Definition/Definition.Api/Definition.Api.csproj"
COPY . .
WORKDIR "/src/Services/Definition/Definition.Api"
RUN dotnet build "Definition.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Definition.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Definition.Api.dll"]