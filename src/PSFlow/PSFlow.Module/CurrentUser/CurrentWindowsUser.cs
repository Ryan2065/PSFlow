using PSFlow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module.CurrentUser
{
    public class CurrentWindowsUser : ICurrentUser
    {
        public string UserName()
        {
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }
    }
}
