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
    
    stage('Test') {
      steps {
        echo "TODO: Implement"
      }
    }
 
    stage('Save') {
      steps {
        archiveArtifacts 'BikeShopAnalyticsAPI/bin/Debug/netcoreapp3.0/BikeShopAnalyticsAPI.dll'
        archiveArtifacts 'BikeShopAnalyticsWebPage/bin/Debug/netcoreapp3.0/BikeShopAnalyticsWebPage.dll'
      }
    }
    
    stage('Merge') {
      steps {
        sh 'git config --global credential.helper cache'
        sh 'git config --global push.default simple'
        sh "git remote set-branches --add origin master"
        sh "git fetch"
        sh "git checkout master"
	    sh "git pull"
        sh "git merge McPeakML"
        sh "git checkout Jenkinsfile master"
        sh "git commit -m "Keep Old Jenkins File""
        sh "git status"
        sh "git push origin master"
      }
    }

  }
  triggers {
    pollSCM('H/15 * * * *')
  }
}
