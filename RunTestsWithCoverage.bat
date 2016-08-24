@echo on
echo Creating output directory
mkdir .\TestResults
echo Installing OpenCover
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
echo Running NUnit tests with OpenCover coverage
.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"nunit3-console.exe"  -targetargs:"ioRPC.Test\bin\Release\ioRPC.Test.dll --result=.\TestResults\Unit.xml" -filter:"+[*]* -[*.Test]*" -register:user -output:".\TestResults\Coverage.xml" -showunvisited