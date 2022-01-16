FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Identity/Career.Identity/Career.Identity.csproj"
WORKDIR "src/Services/Identity/Career.Identity"
RUN dotnet build "Career.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Career.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Career.Identity.dll"]