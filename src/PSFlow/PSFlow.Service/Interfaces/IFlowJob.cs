using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Interfaces
{
    public interface IFlowJob
    {
        public void Start(int flowScriptId);
    }
}
