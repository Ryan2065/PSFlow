using PSFlow.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module.ArgumentCompletors
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FlowName : ArgumentCompleterAttribute, IArgumentCompleter
    {
        private IFlowManager flowManager;
        public FlowName() : base(typeof(FlowName))
        {
            flowManager = (new FlowManagerFactory()).GetManager();
        }
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            var returnResults = new List<CompletionResult>();
            foreach(var result in flowManager.GetFlowNames(wordToComplete))
            {
                returnResults.Add(new CompletionResult(result));
            }
            return returnResults;
        }
    }
}
