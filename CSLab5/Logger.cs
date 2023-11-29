using System;
using System.IO;
using System.Threading;

namespace CSLab5
{
    public class FileLoggerTS
    {
        private static readonly string LogFilePath = "log.txt";
        private static readonly object LockObject = new object();
        private static FileLoggerTS? _instance;

        private FileLoggerTS() { }

        public static FileLoggerTS GetInstance()
        {

            if (_instance == null)
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new FileLoggerTS();
                    }
                }
            }
            return _instance;

        }

        public void LogMessage(string message)
        {
            lock (LockObject)
            {
                string logMessage = $"{DateTime.Now} - {message}";

                File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
            }
        }
    }
}