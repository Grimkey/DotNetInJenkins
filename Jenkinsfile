// Declarative //
pipeline {
  agent {
    label 'windows'
  }
  environment {
    solutionFolder = 'WcfTestSolutionNet35'
    testFolder = "{solutionFolder}\\WcfHealthCheck.Test\\bin\\Debug"
    mstestLoc12 = 'C:\\Program Files (x86)\\Microsoft Visual Studio 12.0\\Common7\\IDE\\mstest.exe'
    msbuild40_64 = 'C:\\Windows\\WinSxS\\amd64_msbuild_b03f5f7f11d50a3a_4.0.15522.0_none_d6821e3da4f1360b\\msbuild.exe'
  }
  options {
    timeout(time: 1, unit: 'HOURS')
  }

  stages {
    stage('Example') {
      steps {
        script {
          powershell "cd {solutionFolder};dir"
        }
      }
    }

    stage('Build') {
      steps {
        script {
          powershell "cd {solutionFolder};{msbuild40_64} /m"
        }
      }
    }
    stage('Test') {
      steps {
        script {
          powershell "cd {testFolder};set-alias mstest '{mstestLoc}';mstest /testcontainer:WcfHealthCheck.Test.dll /resultsfile:TestResults.trx /nologo"
        }
      }
    }
  }
}
