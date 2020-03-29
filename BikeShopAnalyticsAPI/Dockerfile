FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /var/lib/jenkins/workspace/bike-shop-analytics_master@2/
COPY /BikeShopAnalyticsAPI/ BikeShopAnalyticsAPI/
RUN cd BikeShopAnalyticsAPI/
RUN dotnet publish -c Release -r linux-x64 --self-contained false
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/publish/BikeShopAnalyticsAPI.dll"]