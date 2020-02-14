pipeline {
  agent any
  stages {  
    stage('Merge') {
      steps {
        sh 'git config --global credential.helper cache'
        sh 'git config --global push.default simple'
        sh "git checkout origin/master"
        sh "git merge McPeakML" 
        sh "git push origin/master"
      }
    }
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

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}