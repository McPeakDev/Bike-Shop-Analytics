FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine
COPY ./BikeShopAnalyticsAPI /var/www/API
WORKDIR /var/www/API
RUN dotnet publish -c Release -r linux-x64 --self-contained false
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "/var/www/API/bin/Release/netcoreapp3.1/linux-x64/BikeShopAnalyticsAPI.dll"]