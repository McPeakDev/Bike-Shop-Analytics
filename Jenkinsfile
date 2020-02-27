pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        echo 'Changing Directory...'
        sh 'cd BikeShopAnalyticsAPI/ && dotnet build'
        echo 'Building API ...'
        echo 'Build Successful'
      }
    }

    stage('Test') {
      steps {
        echo 'TODO: Implement'
      }
    }

    stage('Save') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'

      }
    }

    stage('Merge') {
      steps {
        sh 'git config --global credential.helper cache'
        sh 'git config --global push.default simple'
        sh 'git remote set-branches --add origin master'
        sh 'git fetch'
        sh 'git checkout master'
        sh 'git pull'
        sh 'git config --global merge.ours.driver true'
        sh 'git merge --no-commit McNabbMR'
        sh 'git checkout HEAD Jenkinsfile'
        sh 'git commit -m \'Merge McNabbMR to master\''
        sh 'git status'
        sh 'git push origin master'
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
