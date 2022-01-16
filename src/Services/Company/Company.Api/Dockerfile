FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Company/Company.Api/Company.Api.csproj"
WORKDIR "/src/src/Services/Company/Company.Api"
RUN dotnet build "Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Api.dll"]