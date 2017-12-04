// Declarative //
pipeline {
  def solutionFolder = 'WcfTestSolutionNet35'
  def mstestLoc = 'C:\\Program Files (x86)\\Microsoft Visual Studio 12.0\\Common7\\IDE\\mstest.exe'
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
          powershell 'cd {solutionFolder};dir'
        }
      }
    }

    stage('Build') {
      steps {
        script {
          powershell 'cd {solutionFolder};C:\\Windows\\WinSxS\\amd64_msbuild_b03f5f7f11d50a3a_4.0.15522.0_none_d6821e3da4f1360b\\msbuild.exe /m'
        }
      }
    }
    stage('Test') {
      steps {
        script {
          powershell 'cd {solutionFolder}\\WcfHealthCheck.Test\\bin\\Debug;set-alias mstest "{mstestLoc}";mstest /testcontainer:WcfHealthCheck.Test.dll /resultsfile:TestResults.trx /nologo'
        }
      }
    }
  }
}