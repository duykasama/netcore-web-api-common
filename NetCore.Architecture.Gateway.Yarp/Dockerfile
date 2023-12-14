FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetCore.Architecture.Gateway.Yarp/NetCore.Architecture.Gateway.Yarp.csproj", "NetCore.Architecture.Gateway.Yarp/"]
RUN dotnet restore "NetCore.Architecture.Gateway.Yarp/NetCore.Architecture.Gateway.Yarp.csproj"
COPY . .
WORKDIR "/src/NetCore.Architecture.Gateway.Yarp"
RUN dotnet build "NetCore.Architecture.Gateway.Yarp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NetCore.Architecture.Gateway.Yarp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCore.Architecture.Gateway.Yarp.dll"]
