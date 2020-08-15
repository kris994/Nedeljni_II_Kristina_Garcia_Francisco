using System;
using System.IO;
using System.Threading;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Logs data to the file, singleton class
    /// </summary>
    class LogManager
    {
        /// <summary>
        /// Stream Writer
        /// </summary>
        private StreamWriter streamWriter;
        /// <summary>
        /// File we are saving the logs at
        /// </summary>
        private readonly string file = @"~\..\..\..\TextFiles\Application.log";
        /// <summary>
        /// Controls the way we enter the file
        /// </summary>
        private EventWaitHandle wait = new AutoResetEvent(true);

        /// <summary>
        /// Single instance of the singleton 
        /// </summary>3
        private static LogManager instance;

        /// <summary>
        /// Lock the instance for thread safety
        /// </summary>
        private static readonly object padlock = new object();

        /// <summary>
        /// Instace method is used to create or reach the single resource (instance)
        /// </summary>
        public static LogManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LogManager();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Private constructor to deny instance creation of this class from outside
        /// </summary>
        private LogManager()
        {

        }

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="message">Message that will be written to the file</param>
        public void WriteLog(string message)
        {
            try
            {
                wait.WaitOne();
                streamWriter = new StreamWriter(file, append: true);
                string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                streamWriter.WriteLine(logMessage.ToString());
                streamWriter.Flush();
                streamWriter.Close();
                wait.Set();
            }
            catch (FileNotFoundException)
            {
                File.Create(file);
            }
        }
    }
}
