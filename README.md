
![Test](https://github.com/netpyoung/NF.Tool.UnityPackage/workflows/Test/badge.svg?branch=master)

https://github.com/TwoTenPvP/UnityPackager
https://github.com/marketplace/actions/create-unitypackage
https://github.com/Cobertos/unitypackage_extractor

dotnet publish -c Release -r win10-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimUnusedDependencies=true

7z a -tzip release.zip %CD%/bin/Release/netcoreapp3.1/win10-x64/publish/*

https://docs.github.com/en/actions/reference/workflow-syntax-for-github-actions#jobsjob_idruns-on
windows-latest


https://github.com/actions/setup-dotnet
3.1.300

https://github.com/<OWNER>/<REPOSITORY>/workflows/<WORKFLOW_FILE_PATH>/badge.svg
