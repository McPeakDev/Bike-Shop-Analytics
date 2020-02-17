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
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/'
        archiveArtifacts 'BikeShopAnalyticsAPI.dll'
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/'
        archiveArtifacts 'BikeShopAnalyticsWebPage.dll'
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
