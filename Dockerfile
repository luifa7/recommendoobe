FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 7126
ENV ASPNETCORE_URLS=http://0.0.0.0:7126

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY *.sln ./
COPY Application/Application.Core.csproj ./Application/
COPY DTOs/DTOs.csproj ./DTOs/
COPY Domain/Domain.Core.csproj ./Domain/
COPY Infrastructure/Infrastructure.Core.csproj ./Infrastructure/

RUN dotnet restore

COPY Application/. ./Application/
COPY DTOs/. ./DTOs/
COPY Domain/. ./Domain/
COPY Infrastructure/. ./Infrastructure/
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Application.Core.dll"]

