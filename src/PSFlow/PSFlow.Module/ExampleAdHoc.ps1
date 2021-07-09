
New-PSFlowJob

#Name is optional?
#return anything from output stream
$Output = Start-PSFlowBlock -Name 'RestartableBlockOne' -ArgumentList @{} -ScriptBlock {

}


$NewOutput = Start-PSFlowBlock -Name 'JobCanStopAndRestart' -ArgumentList $Output -ScriptBlock {
    if($CantContinue){
        Stop-PSFlow -Status 'Waiting'
    }
}

