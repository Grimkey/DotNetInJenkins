// Declarative //
pipeline {
  agent {
    label 'windows'
  }
  options {
    timeout(time: 1, unit: 'HOURS')
  }
  stages {
    stage('Example') {
      steps {
        echo 'Hello World'
      }
    }
    stage('Build') {
      steps {
        script {
          powershell 'Write-Output "Hello, World!" '
        }
      }
    }
  }
}