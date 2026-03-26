FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5113

ENV ASPNETCORE_URLS=http://+:5113

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["csharp-ui.csproj", "./"]
RUN dotnet restore "csharp-ui.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "csharp-ui.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "csharp-ui.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "csharp-ui.dll"]