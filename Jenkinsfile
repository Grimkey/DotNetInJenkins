#!/usr/bin/groovy

// load pipeline functions
// Requires pipeline-github-lib plugin to load library from github
@Library('github.com/campbelldgunn/jenkins-pipeline@master')
def pipeline = new org.whiteshieldinc.Pipeline()

podTemplate(label: 'jenkins-pipeline', nodeSelector: 'os=windows', containers: [
    containerTemplate(name: 'jnlp', image: 'jenkinsci/jnlp-slave:3.14-1-alpine', args: '${computer.jnlpmac} ${computer.name}', workingDir: '/home/jenkins', resourceRequestCpu: '200m', resourceLimitCpu: '300m', resourceRequestMemory: '256Mi', resourceLimitMemory: '512Mi'),
    containerTemplate(name: 'docker', image: 'docker:17.10', command: 'cat', ttyEnabled: true),
    containerTemplate(name: 'aspnetcore', image: 'microsoft/aspnetcore-build:2.0', command: 'cat', ttyEnabled: true),
    containerTemplate(name: 'helm', image: 'campbelldgunn/k8s-helm:v2.7.0', command: 'cat', ttyEnabled: true),
    containerTemplate(name: 'kubectl', image: 'campbelldgunn/k8s-kubectl:v1.8.0', command: 'cat', ttyEnabled: true)
],
volumes:[
    hostPathVolume(mountPath: '/var/run/docker.sock', hostPath: '/var/run/docker.sock'),
]){

  node ('jenkins-pipeline') {

    def pwd = pwd()
    def chart_dir = "${pwd}/charts/orglite"

    checkout scm

    // read in required jenkins workflow config values
    def inputFile = readFile('Jenkinsfile.json')
    def config = new groovy.json.JsonSlurperClassic().parseText(inputFile)
    println "pipeline config ==> ${config}"

    // continue only if pipeline enabled
    if (!config.pipeline.enabled) {
        println "pipeline disabled"
        return
    }

    // set additional git envvars for image tagging
    pipeline.gitEnvVars()

    // If pipeline debugging enabled
    if (config.pipeline.debug) {
      println "DEBUG ENABLED"
      sh "env | sort"

      println "Running kubectl/helm tests"
      container('kubectl') {
        pipeline.kubectlTest()
      }
      container('helm') {
        pipeline.helmConfig()
      }
    }

    def acct = pipeline.getContainerRepoAcct(config)

    // tag image with version, and branch-commit_id
    def tags = pipeline.getContainerTags(config)

    // test stage
    stage ('test') {

      container('aspnetcore') {
          sh "dotnet restore"
          sh "dotnet test ORGLite.Tests --logger:xunit"
          step([$class    : 'XUnitBuilder',
                thresholds: [[$class: 'FailedThreshold', failedThreshold: '1']],
                tools     : [[$class: 'XUnitDotNetTestType', pattern: '**/TestResults.xml']]])
      }
    }

    // compile stage
    stage ('compile') {

      container('aspnetcore') {
          sh "dotnet publish -c Release -o out"
      }
    }

    stage ('test deployment') {

      container('helm') {

        // run helm chart linter
        pipeline.helmLint(chart_dir)

        // run dry-run helm chart installation
        pipeline.helmDeploy(
          dry_run       : true,
          name          : config.app.name,
          namespace     : config.app.name,
          version_tag   : tags.get(0),
          chart_dir     : chart_dir,
          replicas      : config.app.replicas,
          cpu           : config.app.cpu,
          memory        : config.app.memory,
          hostname      : config.app.hostname
        )

      }
    }

    stage ('publish container') {

      container('docker') {

        // perform docker login to azure container registry as the docker-pipeline-plugin doesn't work with the next auth json format
        withCredentials([[$class: 'UsernamePasswordMultiBinding', credentialsId: config.container_repo.jenkins_creds_id,
                        usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD']]) {
          sh "docker login -u ${env.USERNAME} -p ${env.PASSWORD} dfsacr.azurecr.io"
        }

        // build and publish container
        pipeline.containerBuildPub(
            dockerfile: config.container_repo.dockerfile,
            host      : config.container_repo.host,
            acct      : acct,
            repo      : config.container_repo.repo,
            tags      : tags,
            auth_id   : config.container_repo.jenkins_creds_id
        )
      }

    }

    // stage ('deploy to k8s') {
    //   def Boolean bad_things = false
    //   def Boolean dry_run = env.BRANCH_NAME != 'master'
    //   def String name = config.app.name
    //   def String namespace = config.app.name

    //   if (dry_run == true) {
    //     def branch_name = env.BRANCH_NAME.toLowerCase().replace("/", "-")
    //     name = "${name}-${branch_name}"
    //     namespace = "${name}-${branch_name}"
    //   }

    //   container('helm') {
    //     // Deploy using Helm chart
    //     pipeline.helmDeploy(
    //       dry_run       : false,
    //       name          : name,
    //       namespace     : namespace,
    //       version_tag   : tags.get(0),
    //       chart_dir     : chart_dir,
    //       replicas      : config.app.replicas,
    //       cpu           : config.app.cpu,
    //       memory        : config.app.memory,
    //       hostname      : config.app.hostname
    //     )

    //     sh "helm status ${name}"

    //     if (config.app.test) {
    //       try {
    //         pipeline.helmTest(
    //           name          : name
    //         )

    //         if (dry_run == true) {
    //           pipeline.helmDelete(
    //               name       : name
    //           )
    //         }
    //       } catch(Exception ex) {
    //         println "ERROR: Shit went bad yo!"
    //         bad_things = true
    //       }
    //     }
    //   }

    //   if (bad_things == true) {
    //     container('kubectl') {
    //       sh "kubectl logs ${name} --namespace ${namespace}"
    //     }
    //   }
    // }
  }
}