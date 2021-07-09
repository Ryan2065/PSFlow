
if(-not (Test-Path "$PSScriptRoot\bin\release\net5.0\win-x64\publish\Microsoft.EntityFrameworkCore.dll")){
    dotnet publish -c release
}

$files = Get-ChildItem "$PSScriptRoot\bin\release\net5.0\win-x64\publish\"
foreach($file in $files){
    if(-not (Test-Path "$PSScriptRoot\bin\Debug\net5.0\win-x64\$($file.Name)")){
        $null = Copy-Item $file.FullName "$PSScriptRoot\bin\Debug\net5.0\win-x64\" -Force
    }
}

Import-Module "$PSScriptRoot\bin\Debug\net5.0\win-x64\PSFlow.psd1"

Initialize-PSFlow -SQLiteDb -ConnectionString "Data Source=$PSScriptRoot\bin\SQLDb.sqlite;" -SettingsStorage "EnvironmentVariable" -UpdateDatabase

