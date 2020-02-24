pipeline {
  agent any  stages { 
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
        sh "cp BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll ../ "
        archiveArtifacts 'BikeShopAnalyticsAPI.dll'
        sh "cp BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll ../ "
        archiveArtifacts 'BikeShopAnalyticsWebPage.dll'
      }
    }
    
    stage('Deploy') {
      steps {
        withCredentials([usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "ftp $FTPUserName:$FTPPassword@192.168.1.105 -command 'mput BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'        
        }
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
