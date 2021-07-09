# What is PSFlow

PSFlow is a workflow engine for PowerShell that allows you to run ad-hoc or registered workflows. 

The idea of PSFlow is to make code blocks in a script that will only run once per instance. Anything outside of these blocks will always run. 

Example:

``` PowerShell
Import-Module CustomSQLFunctions

$SqlServer = Get-Variable -Name 'Server'

$Output = Start-PSFlowBlock -Name 'Step1' -ScriptBlock {
    Invoke-CustomSql
    Send-MailMessage -Message 'Hey, validate the SQL ran right!'
}

$Output2 = Start-PSFlowBlock -Name 'VerifyApproval' -ScriptBlock {
    $Approval = Validate-Approval
    
    if($false -eq $Approval)) {
        Stop-PSFlow -Status 'Waiting'
    }
}

Start-PSFlowBlock -Name 'Finish' -ScriptBlock {
    Finish-Script
}
```

In the above script, getting the SQL server and importing modules will always run when the script is run. Once Start-PSFlowBlock is run, PSFlow will look at the runspace variables to see if a custom $PSFlowJobId is set. Assuming this is the first time it's run, it won't be set so a new JobId will be created and set in the runspace. The first block will execute, save the ouptut in a database with the JobId, and the continue to step 2. Step 2 will stop the flow because it's validating an approval. It sets the flow to a status of "Waiting" which means it's restartable and then the script exits.

Later, this flow can be restarted with the command

```
Start-PSFlow -Status 'Waiting'
```

All flows in waiting status in the DB will be restarted. The script will execute, run the Import module steps, get the $SqlServer, then hit Step1. Since Step1 already ran and was successful, it will retrieve the output from the DB and return that instead of re-running it. Step two will hit, and it'll check again to see if the approval is done. if it is, the script will continue to step 3.

The database backend can be SQLite or MS SQL Server - so PSFlow can run across multiple servers and track job status (including script output) across any servers it runs on. Just have to run

``` PowerShell
Initialize-PSFlow -SQLiteDb -ConnectionString "Data Source=$PSScriptRoot\bin\SQLDb.sqlite;" -SettingsStorage "EnvironmentVariable" -UpdateDatabase
```

once on each server to set it up. Point them to all the same DB to have jobs be restartable across servers!

This isn't even in an alpha stage yet (DB parts are set up, figuring out the best syntax for the Start-PSFlow stuff), just an idea I'll be chipping away at.

