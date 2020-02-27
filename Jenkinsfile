pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsAPI/ && dotnet publish -c Release --self-contained true --runtime linux-x64
'''
        echo 'Building API ...'
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsWebPage/ && dotnet publish -c Release --self-contained true --runtime linux-x64
'''
        echo 'Building WebApp...'
        echo 'Build Successful'
      }
    }

    stage('Save & Deploy') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.dll, BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.deps.json, BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.runtimeconfig.json '
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json'
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -e 'mput BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.dll BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.deps.json BikeShopAnalyticsWebPage/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsWebPage.runtimeconfig.json; bye' -u $FTPUserName,$FTPPassword 192.168.1.105"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}