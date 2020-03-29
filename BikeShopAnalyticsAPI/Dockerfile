FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine
RUN dotnet publish -c Release -r linux-x64 --self-contained false
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/publish/BikeShopAnalyticsAPI.dll"]