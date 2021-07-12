using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.DB
{
    public static class FlowDbManager
    {
        public static FlowContext GetDbContext()
        {
            if (PSFlow.FlowServiceManager.FlowSettings == null)
            {
                PSFlow.FlowServiceManager.LoadSettings();
            }
            switch (PSFlow.FlowServiceManager.FlowSettings.DbType)
            {
                case "SQLite":
                    return new FlowContextSqlite(PSFlow.FlowServiceManager.FlowSettings.ConnectionString);
                case "SQL":
                    return new FlowContextSQL(PSFlow.FlowServiceManager.FlowSettings.ConnectionString);
            }
            throw new ApplicationException("Could not create the db context - Unexpected FlowSettings.DbType value");
        }
    }
}
