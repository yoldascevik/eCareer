FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR ..
COPY ["src/Services/Definition/Definition.Api/Definition.Api.csproj", "src/Services/Definition/Definition.Api/"]
COPY ["src/Services/Definition/Definition.Application/Definition.Application.csproj", "src/Services/Definition/Definition.Application/"]
COPY ["src/Framework/Career.Mongo/Career.Mongo.csproj", "src/Framework/Career.Mongo/"]
COPY ["src/Framework/Career.Mvc/Career.Mvc.csproj", "src/Framework/Career.Mvc/"]
COPY ["src/Framework/Career.Swagger/Career.Swagger.csproj", "src/Framework/Career.Swagger/"]
COPY ["src/Framework/Career.Logging/Career.Logging.csproj", "src/Framework/Career.Logging/"]
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