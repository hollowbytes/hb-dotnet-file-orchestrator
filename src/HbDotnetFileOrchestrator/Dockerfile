FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HbDotnetFileOrchestrator/HbDotnetFileOrchestrator.csproj", "src/HbDotnetFileOrchestrator/"]
COPY ["src/HbDotnetFileOrchestrator.Infrastructure/HbDotnetFileOrchestrator.Infrastructure.csproj", "src/HbDotnetFileOrchestrator.Infrastructure/"]
COPY ["src/HbDotnetFileOrchestrator.Application/HbDotnetFileOrchestrator.Application.csproj", "src/HbDotnetFileOrchestrator.Application/"]
COPY ["src/HbDotnetFileOrchestrator.Domain/HbDotnetFileOrchestrator.Domain.csproj", "src/HbDotnetFileOrchestrator.Domain/"]
RUN dotnet restore "src/HbDotnetFileOrchestrator/HbDotnetFileOrchestrator.csproj"
COPY . .
WORKDIR "/src/src/HbDotnetFileOrchestrator"
RUN dotnet build "./HbDotnetFileOrchestrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HbDotnetFileOrchestrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HbDotnetFileOrchestrator.dll"]
