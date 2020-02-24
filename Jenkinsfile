pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsAPI/ && dotnet build'
        echo 'Building API ...'
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsWebPage/ && dotnet build'
        echo 'Building WebApp...'
        echo 'Build Successful'
      }
    }

    stage('Save') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'
        archiveArtifacts 'BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll'
      }
    }

    stage('Deploy') {
      steps {
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -v sftp//:$FTPUserName:$FTPPassword@192.168.1.105 -e 'mput BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}