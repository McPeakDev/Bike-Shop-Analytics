pipeline {
  agent any
  stages {
    stage('Change Directory') {
      steps {
        echo 'Changing to API Directory...'
        sh 'cd BikeShopAnalytics/'
      }
    }

    stage('Build') {
      steps {
        echo 'Building API ...'
        sh 'dotnet build'
      }
    }

  }
}