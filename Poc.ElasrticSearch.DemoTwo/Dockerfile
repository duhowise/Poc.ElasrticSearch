#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Poc.ElasrticSearch.DemoTwo/Poc.ElasticSearch.DemoTwo.csproj", "Poc.ElasrticSearch.DemoTwo/"]
RUN dotnet restore "Poc.ElasrticSearch.DemoTwo/Poc.ElasticSearch.DemoTwo.csproj"
COPY . .
WORKDIR "/src/Poc.ElasrticSearch.DemoTwo"
RUN dotnet build "Poc.ElasticSearch.DemoTwo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poc.ElasticSearch.DemoTwo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.ElasticSearch.DemoTwo.dll"]