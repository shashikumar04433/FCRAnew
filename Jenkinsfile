stage('SonarQube analysis') {
            steps {
                script {
                    scannerHome = tool 'Scanner';
                }
                withSonarQubeEnv('SonarQube') { 
                    sh "${/opt/sonar}/bin/linux-x86-64"
                }
            }
        }
