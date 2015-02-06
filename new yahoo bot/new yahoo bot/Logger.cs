using System;
using System.Collections.Generic;
using System.Text;


namespace BotGuruz.Core
{
    public static class Logger
    {
        public  delegate void LogEventDelegate(string value);
        public static event LogEventDelegate LogEvent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text">Pass the string to be logged</param>
        /// <param name="Mode">Verbose = File Logging, Pass Null for simple logging</param>
        public static void LogText(string Text,string Mode)
        {
            if (Mode == "verbose")
            {
                if (LogEvent != null)
                {
                    LogEvent(Text);
                }
            }
            else
            {
                if (LogEvent != null)
                {
                    LogEvent(Text);
                }
            }
        }

       
       
    }
}
