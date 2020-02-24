pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsAPI/ && dotnet build'
        echo 'Building API ...'
        echo 'Changing Directory...'
        echo 'Building WebApp...'
        echo 'Build Successful'
      }
    }

    stage('Save & Deploy') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp sftp//:'$FTPUserName':'$FTPPassword'@192.168.1.105 -c 'mput BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll; bye'"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}