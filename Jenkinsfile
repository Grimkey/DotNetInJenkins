// Declarative //
pipeline {
  agent {
    label 'windows'
  }
  options {
    timeout(time: 1, unit: 'HOURS')
  }
  stages {
    stage('Build') {
      steps {
        script {
          powershell 'cd WcfTestSolutionNet35;C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe /m'
        }
      }
    }
  }
}