#!groovy​

// Ensure Discard Build Property is Set
properties([[$class: 'BuildDiscarderProperty', strategy: [$class: 'LogRotator', artifactDaysToKeepStr: '', artifactNumToKeepStr: '7', daysToKeepStr: '', numToKeepStr: '7']]]);

node {
    slackSend channel: '#dev-builds', message: "Started - ${env.JOB_NAME} ${env.BUILD_NUMBER}. See ${env.BUILD_URL} for more details."
    try {
        stage ("Checkout") {
            checkout scm
            if(isUnix())
            {
                sh "git checkout ${env.BRANCH_NAME}"
            }
            else
            {
                bat "git checkout ${env.BRANCH_NAME}"
            }
        }

        stage ("Build") {

            if(isUnix())
            {
                sh './build.sh'
            }
            else
            {
                bat 'powershell .\\build.ps1'
            }
        }

        slackSend channel: '#dev-engineering-test', color: 'good', message: "Succeeded - ${env.JOB_NAME} ${env.BUILD_NUMBER}. See ${env.BUILD_URL} for more details. @channel"
    }
    catch(err) {
        slackSend channel: '#dev-engineering-test', color: 'danger', message: "Failed - ${env.JOB_NAME} ${env.BUILD_NUMBER}. See ${env.BUILD_URL} for more details. @channel"
        throw err
    }
    finally {
        step([$class: 'WsCleanup'])
    }
}