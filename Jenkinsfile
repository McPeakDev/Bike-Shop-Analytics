pipeline {
  agent any
  stages {
    stage('Create Images') {
      parallel {
        stage('Create API Image') {
          agent any
          environment {
            Home = '/tmp'
          }
          steps {
            sh '''docker build -t api -f BikeShopAnalyticsAPI/Dockerfile .'''
          }
        }

        stage('Create Admin Image') {
          agent any
          steps {
            sh '''docker build -t admin -f bikeshop-admin-frontend/Dockerfile .'''
          }
        }

        stage('Create User Image') {
          agent any
          steps {
            echo 'Implement Image Creation'
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