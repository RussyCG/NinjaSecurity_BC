using Common;
using FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventManagement
{
    public static class EventManager
    {
        /// <summary>
        /// Takes note of an event that has occured from the application
        /// </summary>
        /// <param name="Message">Description of the event</param>
        /// <param name="eventType">Log Level of the event</param>
        public static void ReportNewEvent(string Message, EventTypes eventType)
        {
            // Release process to allow other process to continue to execute
            new Thread(new ThreadStart(() =>
            {
                // Get the current log level assigned to the application from the config
                int iLogLevelOfApplication = AppSettings.LOG_LEVEL;
                // Get the log level of the event
                int iLogLevelOfEvent = (int)eventType;

                // Get the time the event occured
                DateTime dateTimeOfEvent = DateTime.Now;

                // Create event object
                Event occurredEvent = new Event(Message, eventType, dateTimeOfEvent);

                // If the log level of the event is equal to or higher (hierarchical) than that assigned to the application
                if (iLogLevelOfEvent >= iLogLevelOfApplication)
                {
                    // Write to event log
                    new FileHandler.FileHandler().AppendData(AppSettings.EVENT_LOG_PATH, new List<string>() { occurredEvent.ToString() });
                }

                // If the event that occured is an error
                if (iLogLevelOfEvent >= (int)EventTypes.ERROR)
                {
                    // Write to error log
                    new FileHandler.FileHandler().AppendData(AppSettings.ERROR_LOG_PATH, new List<string>() { occurredEvent.ToString() });
                }
            }))
            { Name = "EventReportingThread" }.Start();
        }
    }
}
