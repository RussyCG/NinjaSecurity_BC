using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class AppSettings
    {
        #region Fields

        /// <summary>
        /// Path to the app.config file
        /// </summary>
        private const string APP_CONFIG_PATH = "app.config";
        /// <summary>
        /// private holder for if the settings have been fully loaded into memory
        /// </summary>
        private static bool isFullyLoaded = false;

        #endregion

        #region Properties

        /// <summary>
        /// Path to the error log file
        /// </summary>
        public static string ERROR_LOG_PATH { get; set; }
        /// <summary>
        /// Path to the event log file
        /// </summary>
        public static string EVENT_LOG_PATH { get; set; }
        /// <summary>
        /// Log level of the application
        /// </summary>
        public static int LOG_LEVEL { get; set; }
        
        /// <summary>
        /// Used to verify if all the application settings have been loaded from app.config
        /// </summary>
        public static bool IsFullyLoaded
        {
            get { return isFullyLoaded; }
            set { isFullyLoaded = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start the initialisation of the application
        /// </summary>
        public static void InitialiseSettings()
        {
            new Thread(new ThreadStart(() =>
            {
                Dictionary<string, object> settings = GetKeyValuesForConfig();

                ERROR_LOG_PATH = settings.ContainsKey("ERROR_LOG_PATH") ? (string)settings["ERROR_LOG_PATH"] : null;
                EVENT_LOG_PATH = settings.ContainsKey("EVENT_LOG_PATH") ? (string)settings["EVENT_LOG_PATH"] : null;
                LOG_LEVEL = settings.ContainsKey("LOG_LEVEL") ? (int)settings["LOG_LEVEL"] : -1;


                isFullyLoaded = true;
            })) { Name = "InitialiseSettingsThread" }.Start();
        }

        /// <summary>
        /// Gets key value pair of setting name and value from app.config
        /// </summary>
        /// <returns>Dictionary of settings, key is name of the setting and value is value</returns>
        private static Dictionary<string, object> GetKeyValuesForConfig()
        {
            Dictionary<string, object> returnTemp = new Dictionary<string, object>();
            List<string> temp = new FileHandler.FileHandler().ReadData(APP_CONFIG_PATH);

            for (int i = 0; i < temp.Count; i++)
            {
                try
                {
                    if (temp[i].Trim() == "" || temp[i].Trim().Substring(0, 1) == "#" || temp[i].Trim().Substring(0, 2) == "\\")
                    {
                        continue;
                    }

                    string[] arrSplitter = temp[i].Split('=');

                    string key = arrSplitter[0].Trim();
                    string value = arrSplitter[1].Trim();

                    returnTemp.Add(key, value);
                }
                catch (Exception)
                { continue; }
            }

            return returnTemp;
        }

        #endregion
    }
}
