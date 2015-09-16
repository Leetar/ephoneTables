using System.Diagnostics;

namespace ephoneTables
{
    public class EventLogging
    {
        /// <summary>
        /// Logs message to Windows Event Log. If bool "isError" passed as true, aborts service.
        /// </summary>
        /// <param name="message">Message that will be written to Event Log</param>
        /// <param name="isError">if true, sets event as error and aborts thread</param>
        public static void LogEvent(string message, bool isError)
        {
            EventLog eventLog = new EventLog();
            
            // Check if the event source exists. If not create it.
            if (!EventLog.SourceExists("EphonetablesSource"))
            {
                EventLog.CreateEventSource("EphonetablesSource", "EphoneTablesLog");
            }

            // Set the source name for writing log entries.
            eventLog.Source = "EphonetablesSource";

            // Create an event ID to add to the event log
            if (!isError)
            {
                eventLog.WriteEvent(new EventInstance(8, 4, EventLogEntryType.Information), message);
            }
            else
            {
                eventLog.WriteEvent(new EventInstance(8, 4, EventLogEntryType.Error), message);
            }
            eventLog.Close();
        }
    }
}
