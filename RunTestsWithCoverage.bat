@echo on
echo Creating output directory
mkdir .\TestResults
echo Running NUnit tests with OpenCover coverage
OpenCover.Console.exe -target:"nunit3-console.exe"  -targetargs:"ioRPC.Test\bin\Release\ioRPC.Test.dll --result=.\TestResults\Unit.xml" -filter:"+[*]* -[*.Test]*" -register:user -output:".\TestResults\Coverage.xml" -showunvisited