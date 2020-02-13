pipeline {
  agent any
  stages {
    stage('Merge') {
      environment { 
        GIT_AUTH = credentials('bitbucket-cloud') 
      }
      steps {
        checkout([$class: 'GitSCM', branches: [[name: 'McPeakML']], doGenerateSubmoduleConfigurations: false, extensions: [[$class: 'PreBuildMerge', options: [mergeRemote: 'origin', mergeTarget: 'master']]], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'bitbucket-cloud', url: 'https://bitbucket.org/McPeakML/bike-shop-analytics/']]])
        sh ('''
          git config --local credential.helper "!f() { echo username=\\$GIT_AUTH_USR; echo password=\\$GIT_AUTH_PSW; }; f"
          git push origin master
        ''')
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