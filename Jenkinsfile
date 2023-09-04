pipeline {
    agent any
    stages {
        stage('Git Checkout') {
            steps {
                git url: 'https://github.com/shashikumar04433/FCRAnew.git'    
		            echo "Code Checked-out Successfully!!";
            }
        }
        
        stage('Package') {
            steps {
                bat 'dotnet package'    
		            echo "Dotnet Package Goal Executed Successfully!";
            }
        }
        stage('SonarQube analysis') {
            steps {
		// Change this as per your Jenkins Configuration
                withSonarQubeEnv('SonarQube') {
                    bat 'dotnet build'
                }
            }
        }

    }
}
