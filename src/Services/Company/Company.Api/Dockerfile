FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR ..
COPY ["src/Services/Company/Company.Api/Company.Api.csproj", "src/Services/Company/Company.Api/"]
COPY ["src/Framework/Career.Mvc/Career.Mvc.csproj", "src/Framework/Career.Mvc/"]
COPY ["src/Framework/Career.Exceptions/Career.Exceptions.csproj", "src/Framework/Career.Exceptions/"]
RUN dotnet restore "src/Services/Company/Company.Api/Company.Api.csproj"
COPY . .
WORKDIR "/src/Services/Company/Company.Api"
RUN dotnet build "Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Api.dll"]