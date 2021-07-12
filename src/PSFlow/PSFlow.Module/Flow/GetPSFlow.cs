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
    [Cmdlet("Get","PSFlow", DefaultParameterSetName = "ByName")]
    public class GetPSFlow : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = false, ParameterSetName = "ByName")]
        [FlowName]
        public string Name { get; set; }
        [Parameter(Mandatory = false, ParameterSetName = "ById")]
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
                WriteObject(flowManager.Get(Name));
            }
            else if (MyInvocation.BoundParameters.ContainsKey("Id"))
            {
                WriteObject(flowManager.Get(Id.Value));
            }
            else
            {
                WriteObject(flowManager.Get());
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
