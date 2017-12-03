// Declarative //
pipeline {
  agent {
    label 'linux'
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