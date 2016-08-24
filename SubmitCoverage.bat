@echo on
echo NuGet installing coveralls.net package
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
echo Submitting coverage report to coveralls.io
.\tools\coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i .\TestResults\Coverage.xml --commitAuthor "%APPVEYOR_REPO_COMMIT_AUTHOR%" --commitMessage "%APPVEYOR_REPO_COMMIT_MESSAGE%"
