
New-PSFlowJob

#Name is optional?
#Match on scriptblock and name
#return anything from output stream
$Output = Start-PSFlowBlock -Name '' -ArgumentList @{} -ScriptBlock {

}

$NewOutput = Start-PSFlowBlock -Name '' -ArgumentList @{} -ScriptBlock {
    if($CantContinue){
        Stop-PSFlow -Status 'Waiting'
    }
}

