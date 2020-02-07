pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalytics/ && dotnet build'
        echo 'Building API ...'
      }
    }

    stage('Save') {
      steps {
        archiveArtifacts 'BikeShopAnalytics/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'
      }
    }

  }
}