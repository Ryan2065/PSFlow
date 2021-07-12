@{
    RootModule = 'PSFlow.psm1'
    NestedModules = @("PSFlow.Module.dll")
    RequiredAssemblies = @('Microsoft.EntityFrameworkCore.dll','Microsoft.EntityFrameworkCore.Sqlite.dll','Microsoft.EntityFrameworkCore.SqlServer.dll')
    ModuleVersion = '0.1.0'
    GUID = 'f2afc41b-560e-4dce-86de-e2692938245c'
    Author = 'Ryan Ephgrave'
    CompanyName = 'Ryan Ephgrave'
    Copyright = '(c) Ryan Ephgrave. All rights reserved.'
    Description = 'Great command line editing in the PowerShell console host'
    PowerShellVersion = '7.0'
    AliasesToExport = @()
    #FunctionsToExport = 'PSConsoleHostReadLine'
    CmdletsToExport = 'Initialize-PSFlow', 'Start-PSFlowBlock', 'New-PSFlow', 'Get-PSFlow', 'Remove-PSFlow', 'Set-PSFlow'
    #HelpInfoURI = 'https://aka.ms/powershell71-help'
}