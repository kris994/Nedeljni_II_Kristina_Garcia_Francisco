using System;
using System.IO;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Logs data to the file, singleton class
    /// </summary>
    class LogManager
    {
        private StreamWriter streamWriter;
        private readonly string file = @"~\..\..\..\TextFiles\Application.log";

        /// <summary>
        /// Single instance of the singleton 
        /// </summary>3
        private static LogManager instance;

        /// <summary>
        /// Instace method is used to create or reach the single resource (instance)
        /// </summary>
        public static LogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogManager();
                }

                return instance;
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
                streamWriter = new StreamWriter(file, append: true);
                string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                streamWriter.WriteLine(logMessage.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (FileNotFoundException)
            {
                File.Create(file);
            }
        }
    }
}
