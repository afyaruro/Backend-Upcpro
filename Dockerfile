# Etapa base: imagen de ASP.NET Core (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# Etapa de build: imagen del SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 8.0.0

# Copiar archivos de la solución y proyectos
COPY ["upcpro.sln", "./"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto de los archivos del proyecto
COPY . .

# Usar la imagen de build como final
FROM build AS final
WORKDIR /src/WebAPI

# Comando de inicio - ahora ejecuta dotnet run cuando se inicia el contenedor
CMD ["dotnet", "run"]