
if(-not (Test-Path "$PSScriptRoot\bin\release\net5.0\win-x64\publish\Microsoft.EntityFrameworkCore.dll")){
    dotnet publish -c release
}

$files = Get-ChildItem "$PSScriptRoot\bin\release\net5.0\win-x64\publish\"
foreach($file in $files){
    if(-not (Test-Path "$PSScriptRoot\bin\Debug\net5.0\win-x64\$($file.Name)")){
        $null = Copy-Item $file.FullName "$PSScriptRoot\bin\Debug\net5.0\win-x64\" -Force
    }
}

#Remove-Item "$PSScriptRoot\bin\SQLDb.sqlite" -Force

$FlowTest = "$((Get-Item $PSScriptRoot).Parent.Parent.FullName)\Tests\_RunTests.ps1"
Write-host $FlowTest

. $FlowTest -ModulePath "$PSScriptRoot\bin\Debug\net5.0\win-x64\PSFlow.psd1"

return 

Initialize-PSFlow -SQLiteDb -ConnectionString "Data Source=$PSScriptRoot\bin\SQLDb.sqlite;" -SettingsStorage "EnvironmentVariable" -UpdateDatabase

New-PSFlow -Name 'TestName' -Script {Write-host "Test"}
New-PSFlow -Name 'TestName2' -Script {Write-host "Test"}

Get-PSFlow

Get-PSFlow -Name 'TestName'

Remove-PSFlow -Name 'TestName'

Get-PSFlow