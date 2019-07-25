FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential

WORKDIR /src
COPY ["NuGet.docker.config", "NuGet.config"] 
COPY ["packages", "packages/"]
COPY ["TogglOn.Admin/TogglOn.Admin.csproj", "TogglOn.Admin/"]
RUN dotnet restore "TogglOn.Admin/TogglOn.Admin.csproj"
COPY . .
WORKDIR "/src/TogglOn.Admin"
RUN dotnet build "TogglOn.Admin.csproj" -c Release -o /app

FROM build AS publish
COPY ["TogglOn.Admin/ClientApp/package.json", "TogglOn.Admin/ClientApp/"]
RUN dotnet publish "TogglOn.Admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TogglOn.Admin.dll"]