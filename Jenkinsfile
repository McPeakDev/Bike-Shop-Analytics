pipeline {
  agent any
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
          }
        }

        stage('Test Admin') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            echo 'Implement Testing'
          }
        }

        stage('Test User') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            echo 'Implement Testing'
          }
        }

      }
    }

    stage('Create Images') {
      parallel {
        stage('Create API Image') {
          agent {
            docker {
              image 'mcr.microsoft.com/dotnet/core/aspnet:3.1'
            }

          }
          environment {
            Home = '/tmp'
          }
          steps {
            sh 'apk install docker.io'
            sh '''cd BikeShopAnalyticsAPI/ 
dotnet publish -c Release -r linux-x64 --self-contained false
echo "API Built!"'''
            fileOperations([fileZipOperation('BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/')])
            fileOperations([fileRenameOperation(destination: 'API.zip', source: 'publish.zip')])
            archiveArtifacts 'API.zip'
            sh '''docker build -t API -f BikeShopAnalyticsAPI/Dockerfile BikeShopAnalyticsAPI/.
'''
          }
        }

        stage('Create Admin Image') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            sh '''cd bikeshop-admin-frontend/
npm install 
npm run build
echo "Admin Front-End Built!"'''
            fileOperations([fileZipOperation('bikeshop-admin-frontend/build')])
            fileOperations([fileRenameOperation(destination: 'Admin-FrontEnd.zip', source: 'build.zip')])
            archiveArtifacts 'Admin-FrontEnd.zip'
          }
        }

        stage('Create User Image') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            sh '''cd bikeshop-admin-frontend/
npm install 
npm run build
echo "User Front-End Built!"'''
            fileOperations([fileZipOperation('bikeshop-user-frontend/build')])
            fileOperations([fileRenameOperation(destination: 'User-FrontEnd.zip', source: 'build.zip')])
            archiveArtifacts 'User-FrontEnd.zip'
          }
        }

      }
    }

    stage('Deploy') {
      parallel {
        stage('Deploy API') {
          steps {
            sh '''docker run --publish=443:8080 API
'''
            echo 'API Deployed!'
          }
        }

        stage('Deploy Admin') {
          steps {
            echo 'Admin Front-End Deployed!'
          }
        }

        stage('Deploy User') {
          steps {
            echo 'User Front-End Deployed!'
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