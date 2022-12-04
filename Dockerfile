FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RucksackPrioritization.csproj", "./"]
RUN dotnet restore "RucksackPrioritization.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "RucksackPrioritization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RucksackPrioritization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RucksackPrioritization.dll"]
