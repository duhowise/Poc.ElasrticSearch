#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Poc.ElasrticSearch.DemoOne/Poc.ElasticSearch.DemoOne.csproj", "Poc.ElasrticSearch.DemoOne/"]
RUN dotnet restore "Poc.ElasrticSearch.DemoOne/Poc.ElasticSearch.DemoOne.csproj"
COPY . .
WORKDIR "/src/Poc.ElasrticSearch.DemoOne"
RUN dotnet build "Poc.ElasticSearch.DemoOne.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Poc.ElasticSearch.DemoOne.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poc.ElasticSearch.DemoOne.dll"]