FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Definition/Definition.Api/Definition.Api.csproj"
WORKDIR "/src/src/Services/Definition/Definition.Api"
RUN dotnet build "Definition.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Definition.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Definition.Api.dll"]