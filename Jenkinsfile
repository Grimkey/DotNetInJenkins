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
          //powershell 'cd WcfTestSolutionNet35;C:\\Windows\\Microsoft.NET\\Framework\\v3.5\\msbuild.exe /m'
          powershell 'cd WcfTestSolutionNet35;C:\\Windows\\WinSxS\\amd64_msbuild_b03f5f7f11d50a3a_4.0.15522.0_none_d6821e3da4f1360b\\msbuild.exe /m'
        }
      }

    stage('Test') {
      steps {
        script {
          //powershell 'cd WcfTestSolutionNet35;C:\\Windows\\Microsoft.NET\\Framework\\v3.5\\msbuild.exe /m'
          powershell 'cd WcfTestSolutionNet35\\WcfHealthCheck.Test\\bin\\Debug;C:\\Program Files (x86)\\Microsoft Visual Studio 12.0\\Common7\\IDE\\mstest.exe WcfHealthCheck.Test.dll'
        }
      }
    }
  }
}