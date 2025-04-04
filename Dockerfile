# Etapa base: imagen de ASP.NET Core (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# Etapa de build: imagen del SDK de .NET para compilar la aplicación
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

# Compilar la aplicación
WORKDIR "/src/WebAPI"
RUN dotnet run

#RUN dotnet build -c Release -o /app/build

# Publicar la aplicación
# FROM build AS publish 
#RUN dotnet publish -c Release -o /app/publish

# Imagen final para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando de inicio
CMD ["dotnet", "WebAPI.dll"]
