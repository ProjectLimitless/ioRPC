@echo on
echo Running NUnit tests with OpenCover coverage
OpenCover.Console.exe -target:"nunit3-console.exe"  -targetargs:"ioRPC.Test\bin\Debug\ioRPC.Test.dll --result=.\TestResults\Unit.xml" -filter:"+[*]* -[*.Test]*" -register:user -output:".\TestResults\Coverage.xml" -showunvisited