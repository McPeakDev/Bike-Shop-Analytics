pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
    }

  }
  stages {
    stage('Merge') {
      post {
        failure {
          echo 'Merge Failed Continuing...'
          script {
            currentBuild.result = 'UNSTABLE'
          }

        }

      }
      steps {
        catchError() {
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
        echo 'Changing Directory...'
        sh 'cd bikeshop-user-frontend/ && npm install && npm run build'
        echo 'Building User Front-End ...'
        echo 'Changing Directory...'
        sh 'cd bikeshop-admin-frontend/ && npm install && npm run build'
        echo 'Building Admin Front-End ...'
        echo 'Build Successful'
      }
    }

    stage('Save') {
      steps {
        fileOperations([fileZipOperation('BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/')])
        fileOperations([fileRenameOperation(destination: 'API.zip', source: 'publish.zip')])
        fileOperations([fileZipOperation('bikeshop-admin-frontend/build')])
        fileOperations([fileRenameOperation(destination: 'Admin-FrontEnd.zip', source: 'build.zip')])
        fileOperations([fileZipOperation('bikeshop-user-frontend/build')])
        fileOperations([fileRenameOperation(destination: 'User-FrontEnd.zip', source: 'build.zip')])
        archiveArtifacts 'API.zip, Admin-FrontEnd.zip, User-FrontEnd.zip'
      }
    }

    stage('Deploy') {
      post {
        failure {
          echo 'Useless Errors From FTP...'
          script {
            currentBuild.result = 'UNSTABLE'
          }

        }

      }
      steps {
        echo 'Deployed!'
      }
    }

  }
  environment {
    Home = '/tmp'
  }
  triggers {
    cron('0 8 * * *')
  }
}