node {
  agent {
    label 'linux'
  }
  stages {
    stage('Example') {
      steps {
        echo 'Hello World'
      }
    }
    stage('Build') {
      powershell "dir"
      powershell "msbuild /m"
    }
  }
}