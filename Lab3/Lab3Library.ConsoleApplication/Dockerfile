#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lab3Library.ConsoleApplication/Lab3Library.ConsoleApplication.csproj", "Lab3Library.ConsoleApplication/"]
COPY ["Lab3Library.Core/Lab3Library.Core.csproj", "Lab3Library.Core/"]
RUN dotnet restore "./Lab3Library.ConsoleApplication/./Lab3Library.ConsoleApplication.csproj"
COPY . .
WORKDIR "/src/Lab3Library.ConsoleApplication"
RUN dotnet build "./Lab3Library.ConsoleApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lab3Library.ConsoleApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab3Library.ConsoleApplication.dll"]