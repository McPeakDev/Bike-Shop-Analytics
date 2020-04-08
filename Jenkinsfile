pipeline {
  agent none
  
  environment {
    Home = '/tmp'
  }
  
  stages {
  stage('Build') {
      parallel {
        stage('Build API') {
          agent {
            docker {
              image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
            }

          }
          environment {
            Home = '/tmp'
          }
          steps {
            sh '''cd BikeShopAnalyticsAPI/
                  dotnet publish -c Release -r linux-x64 --self-contained false
                  echo "API Built!"'''
            fileOperations([fileZipOperation('BikeShopAnalyticsAPI/bin/Release/netcoreapp3.1/linux-x64/publish/')])
            fileOperations([fileRenameOperation(destination: 'API.zip', source: 'publish.zip')])
            archiveArtifacts 'API.zip'
          }
        }

        stage('Build Admin') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            echo 'Implement Testing'
            sh '''cd bikeshop-admin-frontend/
                  npm install
                  npm run build
                  echo "Admin Front-End Built!"'''
            fileOperations([fileZipOperation('bikeshop-admin-frontend/build')])
            fileOperations([fileRenameOperation(destination: 'Admin-FrontEnd.zip', source: 'build.zip')])
            archiveArtifacts 'Admin-FrontEnd.zip'
          }
        }

        stage('Build User') {
          agent {
            docker {
              image 'node:10-alpine'
            }

          }
          steps {
            echo 'Implement Testing'
            sh '''cd bikeshop-user-frontend/
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

    stage('Create Images') {
      parallel {
        stage('Create API Images') {
          agent any
          environment {
            Home = '/tmp'
          }
          steps {
            sh 'docker build -t api -f BikeShopAnalyticsAPI/Dockerfile .'
          }
        }

        stage('Create Admin Image') {
          agent any
          steps {
            sh 'docker build -t admin -f bikeshop-admin-frontend/Dockerfile .'
          }
        }

        stage('Create User Image') {
          agent any
          steps {
            sh 'docker build -t user -f bikeshop-user-frontend/Dockerfile .'
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
            sh 'docker run -p 5000:5000 --name api --restart always -d api:latest'
            echo 'API Deployed!'
          }
        }

        stage('Deploy Admin Front-End') {
          agent any
          steps {
            sh 'docker container stop admin-fe && docker container rm admin-fe'
            sh 'docker run -p 3002:3002 --name admin-fe --restart always -d admin:latest'
            echo 'Admin Front-End Deployed!'
          }
        }

        stage('Deploy User Front-End') {
          agent any
          steps {
            sh 'docker container stop user-fe && docker container rm user-fe'
            sh 'docker run -p 3001:3001 --name user-fe --restart always -d user:latest'
            echo 'User Front-End Deployed!'
          }
        }

      }
    }

    stage('Test API') {
      agent {
        docker {
          image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
        }

      }
      steps {
        catchError() {
          sh '''cd BikeShopAnalyticsAPITest/
                dotnet test --logger:trx;LogFileName=./IntegrationTestResults.trx
                 echo "API Tested!"'''
        }
        mstest testResultsFile:"BikeShopAnalyticsAPITest/TestResults/*.trx", keepLongStdio: true
      }
    }

  }
  post {
    always {
        withCredentials([string(credentialsId: 'Discord', variable: 'WebHook')]) {
          discordSend description: "Branch master at ${sh(script:"git rev-parse --short ${GIT_COMMIT}", returnStdout: true)} ${currentBuild.currentResult}", footer: "Commiter: ${sh(script:"git show -s --pretty=%an", returnStdout: true)}", link: "https://bikeshopmonitoring.duckdns.org/jenkins/blue/organizations/jenkins/bike-shop-analytics/detail/master/${BUILD_NUMBER}/pipeline", result: currentBuild.currentResult, title: "Jenkins Pipeline", webhookURL: WebHook
        }
    }
  }
  triggers {
    cron('0 8 * * *')
  }
}