dotnet publish -c Release -r win-x64 --self-contained
copy bin\Release\net7.0\win-x64\publish\workflow.exe ..\workflow.exe