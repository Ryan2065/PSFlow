using PSFlow.DB.Interactions;
using PSFlow.Interfaces;
using PSFlow.Module.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module
{
    public class FlowManagerFactory
    {
        private ICurrentUser _currentUser;
        public FlowManagerFactory()
        {
            _currentUser = (new CurrentUserFactory()).GetCurrentUser();
        }
        public IFlowManager GetManager()
        {
            switch (FlowServiceManager.InteractionMode)
            {
                case FlowInteractionMode.Sql:
                case FlowInteractionMode.WebService:
                default:
                    return new FlowManagerDb(_currentUser);
            }
        }
    }
}
