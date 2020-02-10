pipeline {
  agent any
  stages {
    stage('Merge') {
      steps {
        echo 'Merging McPeakML...'
        sh '''git checkout master
git pull origin McPeakML
git merge McPeakML'''
        echo 'Merge Successful'
        git(url: 'https://McPeakML@bitbucket.org/McPeakML/bike-shop-analytics.git', branch: 'McPeakML')
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