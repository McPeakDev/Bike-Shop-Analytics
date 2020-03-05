pipeline {
  agent any
  stages {
    stage('Merge') {
      steps {
        catchError {
          sh 'git config --global credential.helper cache'
          sh 'git config --global push.default simple'
          sh 'git remote set-branches --add origin McPeakML McNabbMR JohnsonZD hudTest'
          sh 'git fetch'
          sh 'git checkout JohnsonZD'
          sh 'git pull'
          sh 'git checkout McNabbMR'
          sh 'git pull'
          sh 'git checkout hudTest'
          sh 'git pull'
          sh 'git checkout master'
          sh 'git pull'
          sh 'git checkout McPeakML'
          sh 'git pull'
          sh 'git merge origin/JohnsonZD origin/McNabbMR origin/hudTest'
          sh 'git checkout master'
          sh 'git config --global merge.ours.driver true'
          sh 'git merge McPeakML'
          sh 'git status'
          sh 'git push origin master'
          sh 'git checkout McPeakML'
          sh 'git merge master'
          sh 'git push origin McPeakML'
          sh 'git checkout JohnsonZD'
          sh 'git merge master'
          sh 'git push origin JohnsonZD'
          sh 'git checkout McNabbMR'
          sh 'git merge master'
          sh 'git push origin McNabbMR'
          sh 'git checkout hudTest'
          sh 'git merge master'
          sh 'git push origin hudTest'
        }
      }
      post {
        failure {
          echo "Merge Failed Continuing..."
          script {
              currentBuild.result = 'UNSTABLE'
          }
        }
        
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
        sh 'cd BikeShopAnalyticsAPI/ && dotnet publish -c Release -r linux-x64 --self-contained false'
        echo 'Building API ...'
        echo 'Build Successful'
      }
    }
    
    stage('Save') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.dll, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.deps.json, BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json'
      }
    }
    stage('Deploy') {
      steps {
        catchError {
          withCredentials(bindings: [usernamePassword(credentialsId: 'ad99e083-f143-411f-81b1-a87f62c2a72b', usernameVariable: 'FTPUserName', passwordVariable: 'FTPPassword')]) {
          sh "lftp -e 'mput BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.dll BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.deps.json BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/BikeShopAnalyticsAPI.runtimeconfig.json; bye' -u $FTPUserName,$FTPPassword 192.168.1.105"

          }
        }
      }
      post {
        failure {
          echo "Useless Errors From FTP..."
          script {
              currentBuild.result = 'UNSTABLE'
          }
        }
      }
    }
  }
  triggers {
    cron('0 H/2 * * *')
  }
}