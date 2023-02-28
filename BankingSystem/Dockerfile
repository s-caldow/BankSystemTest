FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BankingSystem.csproj", "BankingSystem/"]
RUN dotnet restore "BankingSystem/BankingSystem.csproj"
COPY . .
WORKDIR "/src/BankingSystem"
RUN dotnet build "BankingSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankingSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingSystem.dll"]