pipeline {
  agent any
  stages {  
    stage('Merge') {
      steps {
        sh 'git config --global credential.helper cache'
        sh 'git config --global push.default simple'
        checkout([$class: 'GitSCM', branches: [[name: '*/McPeakML']], doGenerateSubmoduleConfigurations: false, extensions: [[$class: 'PreBuildMerge', options: [mergeRemote: 'origin', mergeTarget: 'master']]], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'bitbucket-cloud', url: 'https://bitbucket.org/McPeakML/bike-shop-analytics/']]])       
        sh "git checkout master" //To get a local branch tracking remote
        sh "git push master" //To get a local branch tracking remote
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