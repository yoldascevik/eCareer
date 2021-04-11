FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR ..
COPY ["src/Services/Job/Job.Api/Job.Api.csproj", "src/Services/Job/Job.Api/"]
COPY ["src/Services/Job/Job.Application/Job.Application.csproj", "src/Services/Job/Job.Application/"]
COPY ["src/Framework/Career.Exceptions/Career.Exceptions.csproj", "src/Framework/Career.Exceptions/"]
COPY ["src/Framework/Career.IoC/Career.IoC.csproj", "src/Framework/Career.IoC/"]
COPY ["src/Framework/Career.Logging/Career.Logging.csproj", "src/Framework/Career.Logging/"]
COPY ["src/Framework/Career.Mvc/Career.Mvc.csproj", "src/Framework/Career.Mvc/"]
COPY ["src/Framework/Career.Swagger/Career.Swagger.csproj", "src/Framework/Career.Swagger/"]
RUN dotnet restore "src/Services/Job/Job.Api/Job.Api.csproj"
COPY . .
WORKDIR "/src/Services/Job/Job.Api"
RUN dotnet build "Job.Api.csproj" -c Release -o /app/build

#FROM build AS test 
#RUN dotnet test -c Release ../Job.Tests/Job.Tests.csproj

FROM build AS publish
RUN dotnet publish "Job.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job.Api.dll"]