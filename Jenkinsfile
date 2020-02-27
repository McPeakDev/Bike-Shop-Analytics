pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsAPI/ && dotnet publish -c Release --self-contained true --runtime linux-x64
'''
        echo 'Building API ...'
        echo 'Build Successful'
      }
    }

    stage('Save & Deploy') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json'
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -e 'mput BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json; bye' -u $FTPUserName,$FTPPassword 192.168.1.105"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
