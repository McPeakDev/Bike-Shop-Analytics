FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY BikeShopAnalyticsAPI/ BikeShopAnalyticsAPI/
RUN cd BikeShopAnalyticsAPI/
RUN dotnet publish -c Release -r linux-x64 --self-contained false
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/publish/BikeShopAnalyticsAPI.dll"]