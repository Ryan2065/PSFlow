using System;
using Newtonsoft.Json;

namespace PSFlow
{
    public static class FlowServiceManager
    {
        public static FlowSettings FlowSettings { get; set; }
        public static string PSFlowEnivronment
        {
            get
            {
                return Environment.GetEnvironmentVariable("PSFlow_Environment");
            }
        }

        public static FlowInteractionMode InteractionMode;

        public static void LoadSettings()
        {
            var settingsJson = Environment.GetEnvironmentVariable("PSFlow_Settings");
            if (String.IsNullOrEmpty(settingsJson))
            {
                var settingsFilePath = Environment.GetEnvironmentVariable("PSFlow_SettingsFileLocation");
                if (!string.IsNullOrEmpty(settingsFilePath))
                {
                    if (System.IO.File.Exists(settingsFilePath))
                    {
                        using (var sr = new System.IO.StreamReader(settingsFilePath))
                        {
                            settingsJson = sr.ReadToEnd();
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(settingsJson))
            {
                throw new ApplicationException("Settings not found. Please run Initialize-PSFlow to set settings.");
            }
            FlowSettings = JsonConvert.DeserializeObject<FlowSettings>(settingsJson);
        }
        private static void SetEnvironmentVaraible(string varName, string value)
        {
            var currentValue = Environment.GetEnvironmentVariable(varName, EnvironmentVariableTarget.User);
            if(!String.Equals(currentValue, value, StringComparison.OrdinalIgnoreCase))
            {
                Environment.SetEnvironmentVariable(varName, value, EnvironmentVariableTarget.User);
            }
        }
        public static void SaveSettingsInFile(string filePath, FlowSettings settings)
        {
            SetEnvironmentVaraible("PSFlow_Settings", null);
            SetEnvironmentVaraible("PSFlow_SettingsFileLocation", filePath);
            System.IO.File.WriteAllText(filePath, settings.ConvertToJson());
        }
        public static void SaveSettingsInEnvironment(FlowSettings flowSettings)
        {
            SetEnvironmentVaraible("PSFlow_SettingsFileLocation", null);
            SetEnvironmentVaraible("PSFlow_Settings", flowSettings.ConvertToJson());
        }
    }
    public class FlowSettings
    {
        public string ConnectionString { get; set; }
        public string DbType { get; set; }
        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
