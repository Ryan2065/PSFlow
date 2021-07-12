using PSFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module.Flow
{
    [Cmdlet("New", "PSFlow")]
    public class NewPSFlow : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = true)]
        public string Name { get; set; }
        [Parameter(Mandatory = true)]
        public string Script { get; set; }
        [Parameter(Mandatory = false)]
        public string Description { get; set; }
        public SwitchParameter Publish { get; set; }
        #endregion

        private IFlowManager flowManager;
 
        protected override void BeginProcessing()
        {
            flowManager = (new FlowManagerFactory()).GetManager();
        }
        protected override void ProcessRecord()
        {
            WriteObject(flowManager.New(Name, Script, Description, Publish.IsPresent));
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
