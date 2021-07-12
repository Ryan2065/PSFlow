Param(
    [string]$ModulePath
)

Import-MOdule Pester -MinimumVersion 5.0
Import-Module $ModulePath -Force
Remove-Item "$PSScriptRoot\bin\SQLDb.sqlite" -Force -ErrorAction SilentlyContinue

Initialize-PSFlow -SQLiteDb -ConnectionString "Data Source=$PSScriptRoot\bin\SQLDb.sqlite;" -SettingsStorage "EnvironmentVariable" -UpdateDatabase


Invoke-Pester "$PSScriptRoot\FlowTests.tests.ps1"