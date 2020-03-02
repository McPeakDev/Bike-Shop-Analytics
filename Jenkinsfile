pipeline {
  agent any
  stages {
    stage('Merge') {
      steps {
        sh 'git config --global credential.helper cache'
        sh 'git config --global push.default simple'
        sh 'git remote set-branches --add origin McPeakML McNabbMR JohnsonZD hudTest'
        sh 'git fetch'
        sh 'git pull'
        sh 'git checkout McPeakML'
        sh 'git checkout JohnsonZD'
        sh 'git checkout McNabbMR'
        sh 'git checkout hudTest'
        sh 'git checkout master'
        sh 'git config --global merge.ours.driver true'
        sh 'git merge origin/McPeakML origin/JohnsonZD origin/McNabbMR origin/hudTest --no-commit'
        sh 'git checkout HEAD Jenkinsfile'
        sh 'git commit -m \'Merge all dev branches to master\''
        sh 'git status'
        sh 'git push origin master'
        sh 'git push origin McPeakML'
        sh 'git push origin JohnsonZD'
        sh 'git push origin McNabbMR'
        sh 'git push origin hudTest'
      }
    }
    
    stage('Test') {
      steps {
        echo 'Implement Testing'
      }
    }
  
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh '''cd BikeShopAnalyticsAPI/ && dotnet publish -c Release --self-contained true --runtime linux-x64'''
        echo 'Building API ...'
        echo 'Build Successful'
      }
    }

    stage('Save & Deploy') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json'
        withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -e 'mput BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.dll BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.deps.json BikeShopAnalyticsAPI/bin/Release/netcoreapp3.0/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json; bye' -u $FTPUserName,$FTPPassword 192.168.1.105"
        }

      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
