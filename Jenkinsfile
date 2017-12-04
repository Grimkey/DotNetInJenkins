
// load pipeline functions
// Requires pipeline-github-lib plugin to load library from github
//@Library('github.com/aweiker/jenkins-pipeline@helm-upgrade')
//def pipeline = new org.whiteshieldinc.Pipeline()

pipeline {
  agent {
    label 'windows'
  }
  environment {
    solutionFolder = 'WcfTestSolutionNet35'
    testFolder = "WcfTestSolutionNet35\\WcfHealthCheck.Test\\bin\\Debug"
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
          powershell "cd ${env.solutionFolder};dir"
        }
      }
    }
    stage('Clean') {
      steps {
        script {
          powershell "cd ${env.solutionFolder};${env.msbuild40_64} /m /t:Clean"
        }
      }
    }
    stage('Build') {
      steps {
        script {
          powershell "cd ${env.solutionFolder};${env.msbuild40_64} /m"
        }
      }
    }
    stage('Test') {
      steps {
        script {
          powershell """
            cd ${env.testFolder};
            del TestResults.trx
            set-alias mstest '${env.mstestLoc12}';
            mstest /testcontainer:WcfHealthCheck.Test.dll /resultsfile:TestResults.trx /nologo
            """
        }
      }
    }
    stage('Publish') {
      environment {
        DOCKERKEY = credentials('dfsacr')
      }
      steps {
        powershell "'Do work here'"
      }
    }
  }
}
