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
        script {
          powershell 'cd WcfTestSolutionNet35;dir'
        }
      }
    }
    stage('Build') {
      steps {
        script {
          powershell 'cd WcfTestSolutionNet35;msbuild /m'
        }
      }
    }
  }
}