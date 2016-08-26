Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "$PSScriptRoot\build\nuget.exe"

& "$PSScriptRoot\build\nuget.exe" "Install" "FAKE" "-OutputDirectory" "build" "-ExcludeVersion"
& "$PSScriptRoot\build\FAKE\tools\Fake.exe" "$PSScriptRoot\build\build.fsx"
