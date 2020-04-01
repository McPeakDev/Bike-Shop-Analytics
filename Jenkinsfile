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
          withCredentials(bindings: [usernamePassword(credentialsId: 'fd3bf359-9733-41e4-a86d-be304b4c1c49', passwordVariable: 'GIT_PASS', usernameVariable: 'GIT_USER')]) {
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
            sh 'git remote set-url origin ssh://git@bitbucket.org/$GIT_USER/bike-shop-analytics.git'
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
    
    stage('Deploy Test API') {
      agent {
          docker {
            image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
           }
      }
      steps {
         sh 'docker container stop api'
          sh 'docker run -p 5000:5000 --name api-test --restart always -d api:latest'
          echo 'API Deployed!'
       }
    }
    
    stage('Test API') {
      agent {
          docker {
            image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
          }
      }
      steps {
          sh '''cd BikeShopAnalyticsAPITest/
                dotnet test
                 echo "API Tested!"'''   
      }
      post {
          failure {
              sh 'docker container stop api-test && docker container start api'
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

  }
  environment {
    Home = '/tmp'
  }
  triggers {
    cron('0 8 * * *')
  }
}