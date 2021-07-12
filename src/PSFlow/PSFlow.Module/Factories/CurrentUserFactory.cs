using PSFlow.Interfaces;
using PSFlow.Module.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module.Factories
{
    public class CurrentUserFactory
    {
        public ICurrentUser GetCurrentUser()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new CurrentWindowsUser();
            }
            throw new ApplicationException("Unsupported operating system");
        }
    }
}
