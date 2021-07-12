using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PSFlow.Interfaces
{
    public interface IFlowManager : IDisposable
    {
        public PSFlow.Models.Flow New(string name, string script, string description, bool publish = false);
        public PSFlow.Models.Flow Get(string name);
        public PSFlow.Models.Flow Get(int id);
        public List<PSFlow.Models.Flow> Get();
        public List<string> GetFlowNames(string nameContains);
        public void Remove(int id);
        public void Remove(string name);
        public PSFlow.Models.Flow Set(int id, PSFlow.Models.Flow newObject);
    }
}
