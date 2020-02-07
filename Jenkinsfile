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

  }
}