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
    
    stage('Deploy') {
      steps {
        ftpPublisher alwaysPublishFromMaster: false, continueOnError: false, failOnError: false, publishers: [[configName: 'Deployment', transfers: [[asciiMode: false, cleanRemote: false, excludes: '', flatten: false, makeEmptyDirs: false, noDefaultExcludes: false, patternSeparator: '[, ]+', remoteDirectory: '', remoteDirectorySDF: false, removePrefix: '', sourceFiles: '*.jar']], usePromotionTimestamp: false, useWorkspaceInPromotion: false, verbose: false]]
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
