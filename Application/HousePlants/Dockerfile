FROM mcr.microsoft.com/dotnet/aspnet AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /src
COPY ["HousePlants.csproj", "."]
RUN dotnet restore "HousePlants.csproj"
COPY . .
RUN dotnet build "HousePlants.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HousePlants.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HousePlants.dll"]
