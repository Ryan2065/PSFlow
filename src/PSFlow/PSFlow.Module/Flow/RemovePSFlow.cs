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
    [Cmdlet("Remove", "PSFlow", DefaultParameterSetName = "ByName")]
    public class RemovePSFlow : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = true, ParameterSetName = "ByName")]
        [FlowName]
        public string Name { get; set; }
        [Parameter(Mandatory = true, ParameterSetName = "ById")]
        public int? Id { get; set; }
        #endregion
        private IFlowManager flowManager;

        protected override void BeginProcessing()
        {
            flowManager = (new FlowManagerFactory()).GetManager();
        }
        protected override void ProcessRecord()
        {
            if (MyInvocation.BoundParameters.ContainsKey("Name"))
            {
                flowManager.Remove(Name);
            }
            else if (MyInvocation.BoundParameters.ContainsKey("Id"))
            {
                flowManager.Remove(Id.Value);
            }
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
