FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gateways/Gateway.Web.Api/Gateway.Web.Api.csproj", "Gateway.Web.Api/"]
RUN dotnet restore "src/Gateways/Gateway.Web.Api/Gateway.Web.Api.csproj"
COPY . .
WORKDIR "/src/Gateway.Web.Api"
RUN dotnet build "Gateway.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Web.Api.dll"]
