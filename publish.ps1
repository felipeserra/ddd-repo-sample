$workspace = (Get-Item -Path ".\" -Verbose).FullName
cd $workspace/src
dotnet publish -c Release -o $workspace/publish
