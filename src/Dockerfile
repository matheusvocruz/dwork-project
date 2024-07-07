FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DWorks.Api/DWorks.Api.csproj", "DWorks.Api/"]
COPY ["Dworks.Application/Dworks.Application.csproj", "Dworks.Application/"]
COPY ["DWorks.Domain/DWorks.Domain.csproj", "DWorks.Domain/"]
COPY ["DWorks.Infra.Data/DWorks.Infra.Data.csproj", "DWorks.Infra.Data/"]
COPY ["DWorks.Infra.IoC/DWorks.Infra.IoC.csproj", "DWorks.Infra.IoC/"]
RUN dotnet restore "DWorks.Api/DWorks.Api.csproj"
COPY . .
WORKDIR "/src/DWorks.Api/"
RUN dotnet build "DWorks.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DWorks.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DWorks.Api.dll"]