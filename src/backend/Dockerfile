FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["StudentProjects.API/StudentProjects.API.csproj", "StudentProjects.API/"]
COPY ["StudentProjects.Domain/StudentProjects.Domain.csproj", "StudentProjects.Domain/"]
COPY ["StudentProjects.Models/StudentProjects.Models.csproj", "StudentProjects.Models/"]
COPY ["StudentProjects.Application/StudentProjects.Application.csproj", "StudentProjects.Application/"]
COPY ["StudentProjects.DataLayer/StudentProjects.DataLayer.csproj", "StudentProjects.DataLayer/"]
RUN dotnet restore "StudentProjects.API/StudentProjects.API.csproj"
COPY . .
WORKDIR "/src/StudentProjects.API"
RUN dotnet build "StudentProjects.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentProjects.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentProjects.API.dll"]