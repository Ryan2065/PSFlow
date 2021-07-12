using Microsoft.EntityFrameworkCore;
using PSFlow.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Module
{
    [Cmdlet(VerbsData.Initialize, "PSFlow")]
    public class InitializePSFlow : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = true, ParameterSetName = "SQL")]
        public SwitchParameter SQLDb { get; set; }
        [Parameter(Mandatory = true, ParameterSetName = "SQLite")]
        public SwitchParameter SQLiteDb { get; set; }
        [Parameter(Mandatory = true, ParameterSetName = "SQL", HelpMessage = "Connection string in the form of Server=serverName;Database=dbName;Trusted_Connection=True;")]
        [Parameter(Mandatory = true, ParameterSetName = "SQLite", HelpMessage = "Connection string in the form of Data Source=FullFilePath;")]
        public string ConnectionString { get; set; }
        [ValidateSet("EnvironmentVariable", "File", "None")]
        [Parameter(Mandatory = false, HelpMessage = "Should the settings be stored so this doesn't have to be called again.")]
        public string SettingsStorage { get; set; } = "None";
        [Parameter(Mandatory = false, HelpMessage = "If storing settings in a file - where should they be stored. This file path will be stored in the user environment variable PSFlow_SettingsFileLocation")]
        [ValidateNotNullOrEmpty()]
        public string SettingsFilePath { get; set; }
        [Parameter(Mandatory =false, HelpMessage = "Should the database be created / updated?")]
        public SwitchParameter UpdateDatabase { get; set; }
        #endregion

        private FlowSettings _flowSettings;

        protected override void BeginProcessing()
        {
            var flowSettings = new FlowSettings();
            flowSettings.ConnectionString = ConnectionString;
            if (SQLDb.IsPresent)
            {
                flowSettings.DbType = "SQL";
            }
            else if (SQLiteDb.IsPresent)
            {
                flowSettings.DbType = "SQLite";
            }
            _flowSettings = flowSettings;
        }
        protected override void ProcessRecord()
        {
            FlowServiceManager.FlowSettings = _flowSettings;
            switch (SettingsStorage)
            {
                case "EnvironmentVariable":
                    FlowServiceManager.SaveSettingsInEnvironment(_flowSettings);
                    break;
                case "File":
                    if (String.IsNullOrEmpty(SettingsFilePath))
                    {
                        throw new ArgumentNullException("SettingsFilePath parameter required with File setting storage");
                    }
                    FlowServiceManager.SaveSettingsInFile(SettingsFilePath, _flowSettings);
                    break;
                case "None":
                default:
                    break;
            }
            if (UpdateDatabase.IsPresent)
            {
                var db = FlowDbManager.GetDbContext();
                db.Database.Migrate();
                db.Dispose();
            }
        }
        protected override void EndProcessing()
        {
            
        }
        protected override void StopProcessing()
        {
            EndProcessing();
        }
    }
}
