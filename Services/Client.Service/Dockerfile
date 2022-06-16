FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/app
COPY ["./Clients.Service.csproj", "./"]
RUN dotnet restore "Clients.Service.csproj"
COPY ["./", "./"]
RUN dotnet build "Clients.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clients.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clients.Service.dll"]