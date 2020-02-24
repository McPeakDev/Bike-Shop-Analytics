pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsAPI/ && dotnet publish --self-contained true --runtime linux-x64 --framework netcoreapp3.0
'''
        echo 'Building API ...'
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsWebPage/ && dotnet publish --self-contained true --runtime linux-x64 --framework netcoreapp3.0
'''
        echo 'Building WebApp...'
        echo 'Build Successful'
      }
    }

    stage('Save & Deploy') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.dll, BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.deps.json, BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.runtimeconfig.json '
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.dll, BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.deps.json, BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.runtimeconfig.json'
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -e 'mput BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.dll BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.deps.json BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsAPI.runtimeconfig.json BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.dll BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.deps.json BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/linux-x64/BikeShopAnalyticsWebPage.runtimeconfig.json; bye' -u $FTPUserName,$FTPPassword 192.168.1.105"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}