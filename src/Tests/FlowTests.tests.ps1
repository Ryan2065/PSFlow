
$count = 0
$FlowNames = @()
while($count -lt 10){
    $FlowNames += @((New-GUID).ToString())
    $count++
}

Describe 'Creating Flows'{
    $Name = $FlowNames[0]
    $Output = New-PSFlow -Name $Name -Script { Write-host "Test" }
    $GetByNameFlow = Get-PSFlow -Name $Name
    $GetById = Get-PSFlow -Id 1
    $NewFlow = Set-PSFlow -Id 1 -NewName 'NewName'
    It 'Should successfully create a flow'{
        $output.Name | Should -be $Name
    }
    It 'Should get the flow by name'{
        ($GetByNameFlow).Name | Should -be $Name
    }
    It 'Should get flow by id'{
        ($GetById).Name | Should -be $Name
    }
    It 'Should set a new name'{
        (Get-PSFlow -Id 1).Name | Should -be 'NewName'
    }
    Remove-PSFlow -Id 1
    It 'Should remove flow'{
        (Get-PSFlow -Id 1).Deleted | Should -be $true
    }
}