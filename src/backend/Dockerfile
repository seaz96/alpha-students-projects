FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["StudentsProjects/StudentsProjects.API.csproj", "StudentsProjects.API/"]
COPY ["StudentsProjects/StudentsProjects.Domain.csproj", "StudentsProjects.Domain/"]
RUN dotnet restore "StudentsProjects/StudentsProjects.API.csproj"
COPY . .
WORKDIR "/src/StudentsProjects"
RUN dotnet build "StudentsProjects.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentsProjects.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentsProjects.API.dll"]