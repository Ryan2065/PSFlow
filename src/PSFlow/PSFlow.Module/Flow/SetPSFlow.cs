using PSFlow.Interfaces;
using PSFlow.Module.ArgumentCompletors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module.Flow
{
    [Cmdlet("Set", "PSFlow")]
    public class SetPSFlow : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = true, ParameterSetName = "ByName")]
        [FlowName]
        public string Name { get; set; }
        [Parameter(Mandatory = true, ParameterSetName = "ById")]
        public int? Id { get; set; }
        [Parameter(Mandatory = false, ParameterSetName = "ByName")]
        [Parameter(Mandatory = false, ParameterSetName = "ById")]
        public string NewName { get; set; }
        [Parameter(Mandatory = false, ParameterSetName = "ByName")]
        [Parameter(Mandatory = false, ParameterSetName = "ById")]
        public string Description { get; set; }
        [Parameter(Mandatory = false, ParameterSetName = "ByName")]
        [Parameter(Mandatory = false, ParameterSetName = "ById")]
        public int? ActiveScriptId { get; set; }
        #endregion
        private IFlowManager flowManager;

        protected override void BeginProcessing()
        {
            flowManager = (new FlowManagerFactory()).GetManager();
        }
        protected override void ProcessRecord()
        {
            PSFlow.Models.Flow oldFlow = null;
            if (MyInvocation.BoundParameters.ContainsKey("Name"))
            {
                oldFlow = flowManager.Get(Name);
            }
            else if (MyInvocation.BoundParameters.ContainsKey("Id"))
            {
                oldFlow = flowManager.Get(Id.Value);
            }
            if (MyInvocation.BoundParameters.ContainsKey("NewName"))
            {
                oldFlow.Name = NewName;
            }
            if (MyInvocation.BoundParameters.ContainsKey("ActiveScriptId"))
            {
                oldFlow.ActiveScriptId = ActiveScriptId;
            }
            if (MyInvocation.BoundParameters.ContainsKey("Description"))
            {
                oldFlow.Description = Description;
            }
            WriteObject(flowManager.Set(oldFlow.FlowId, oldFlow));
            base.ProcessRecord();
        }

        private void CleanUp()
        {
            flowManager.Dispose();
        }

        protected override void EndProcessing()
        {
            CleanUp();
            base.EndProcessing();
        }
        protected override void StopProcessing()
        {
            CleanUp();
            base.StopProcessing();
        }
    }
}
