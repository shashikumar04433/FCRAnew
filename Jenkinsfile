pipeline {
    agent any

    environment {
        DOTNET_HOME = tool name: 'dotnet-sdk-7.0', type: 'Tool'
    }

    stages {
        stage('Git Checkout') {
            steps {
                git url: 'https://github.com/shashikumar04433/FCRAnew.git'    
		            echo "Code Checked-out Successfully!!";
            }
            }
        }

        stage('Build') {
            steps {
                // Use the .NET SDK to build your project
                sh "${/opt/sonar}/bin/linux-x86-64"
            }
        }

        stage('Publish Artifacts') {
            steps {
                // Publish build artifacts, if needed
                archiveArtifacts artifacts: '**/bin/**/*', allowEmptyArchive: true
            }
        }
