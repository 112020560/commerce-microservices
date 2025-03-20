FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workers/EventSourcing.Worker/EventSourcing.Worker.csproj", "Workers/EventSourcing.Worker/"]
RUN dotnet restore "Workers/EventSourcing.Worker/EventSourcing.Worker.csproj"
COPY . .
WORKDIR "/src/Workers/EventSourcing.Worker"
RUN dotnet build "EventSourcing.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EventSourcing.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventSourcing.Worker.dll"]
