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
        sh "cp BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll ../ "
        archiveArtifacts 'BikeShopAnalyticsAPI.dll'
        sh "cp BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll ../ "
        archiveArtifacts 'BikeShopAnalyticsWebPage.dll'
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
