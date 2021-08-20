FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/CV/CV.Api/CV.Api.csproj"
WORKDIR "/src/src/Services/CV/CV.Api"
RUN dotnet build "CV.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CV.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CV.Api.dll"]