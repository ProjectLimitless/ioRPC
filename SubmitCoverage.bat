@echo on
echo NuGet installing coveralls.net package
dir
dir "TestResults"
nuget install coveralls.net -Version 0.7.0 -OutputDirectory tools
echo Submitting coverage report to coveralls.io
.\tools\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\TestResults\Coverage.xml --commitAuthor "%APPVEYOR_REPO_COMMIT_AUTHOR%" --commitMessage "%APPVEYOR_REPO_COMMIT_MESSAGE%" --commitId "%APPVEYOR_REPO_COMMIT%"
