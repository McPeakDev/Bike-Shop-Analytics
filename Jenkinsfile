pipeline {
  agent none
  stages {
    stage('Merge') {
      agent any
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
        withCredentials([usernamePassword(credentialsId: 'bitbucket-cloud', passwordVariable: 'GIT_PASS', usernameVariable: 'GIT_USER')]) {
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
          sh 'git remote set-url origin https://bitbucket.org/$GIT_USER/bike-shop-analytics.git'
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
    }

    stage('Test') {
      parallel {
        stage('Test API') {
          agent {
            docker {
              image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
            }

          }
          environment {
            Home = '/tmp'
          }
          steps {
            echo 'Implement Testing'
            sh '''cd BikeShopAnalyticsAPI/
                  dotnet publish -c Release -r linux-x64 --self-contained false
                  echo "API Built!"'''
            fileOperations([fileZipOperation('BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/')])
            fileOperations([fileRenameOperation(destination: 'API.zip', source: 'publish.zip')])
            archiveArtifacts 'API.zip'
          }
        }

        stage('Test Front-End') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            echo 'Implement Testing'
            sh '''cd bikeshop-frontend/
                  npm install
                  npm run build
                  echo "Front-End Built!"'''
            fileOperations([fileZipOperation('bikeshop-admin-frontend/build')])
            fileOperations([fileRenameOperation(destination: 'Admin-FrontEnd.zip', source: 'build.zip')])
            archiveArtifacts 'Admin-FrontEnd.zip'
          }
        }

      }
    }

    stage('Create Images') {
      parallel {
        stage('Create API Image') {
          agent any
          environment {
            Home = '/tmp'
          }
          steps {
            sh 'docker build -t api -f BikeShopAnalyticsAPI/Dockerfile .'
          }
        }

        stage('Create Front-End Image') {
          agent any
          steps {
            sh 'docker build -t front-end -f bikeshop-frontend/Dockerfile .'
          }
        }

      }
    }

    stage('Deploy') {
      parallel {
        stage('Deploy API') {
          agent any
          steps {
            sh 'docker container stop api && docker container rm api'
            sh 'docker run -p 5000:5000 --name api -d api:latest'
            echo 'API Deployed!'
          }
        }

        stage('Deploy Front-End') {
          agent any
          steps {
            sh 'docker container stop front-end && docker container rm front-end'
            sh 'docker run -p 3000:3000 --name front-end -d front-end:latest'
            echo 'Admin Front-End Deployed!'
          }
        }

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