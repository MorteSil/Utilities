using System.Reflection;
using System.Text;

namespace Utilities.Logging
{
    /// <summary>
    /// Class containing methods to handle error logging
    /// </summary>
    public static class ErrorLog
    {
        private static Mutex mutex = new();

        /// <summary>
        /// Creates a log file and includes information contained in <paramref name="ex"/>.
        /// </summary>
        /// <param name="ex">An <see cref="Exception"/> that with information that needs to be included in the log file.</param>
        /// <param name="details">Optional additional details that can be added to the Log Entry</param>
        /// <param name="logFolder">Log Output Folder. If none is supplied, the default location is a Logs Directory created in the Application Installation Directory.\</param>
        /// <param name="appendToExisting">When <see langword="true"/>, the logger will append the log to an existing log file with the current Date Value.</param>
        /// <param name="severity">The severity of the issue being logged.</param>
        public static void CreateLogFile(Exception? ex, string? details = null, string? logFolder=null, bool appendToExisting = true, LogSeverity severity = LogSeverity.Error)
        {
            // DateTime the event occurred
            DateTime timeStamp = DateTime.Now;

            // Setup the target location.
            string outputFolder = logFolder ?? string.Empty;
            string outputFile = (appendToExisting ? timeStamp.ToString("yyyy-MM-dd") : timeStamp.ToString("yyyy-MM-dd-hh-mm-ss"));
            if (string.IsNullOrEmpty(outputFolder)) { outputFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Logs"; }

            string fileName = outputFolder + "\\" + outputFile;
            // Create the File if needed and setup the FileInfo
            mutex.WaitOne();
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
            if (!File.Exists(fileName + ".log"))
                File.Create(fileName + ".log").Close();
            else
            {
                if (appendToExisting) { fileName += ".log"; }
                else
                {                    
                    int i = 1;
                    while (File.Exists(fileName + "_" + i++ + ".log"))
                        ;
                    fileName += "_" + i + ".log";
                    File.Create(fileName).Close();
                }
            }

            //Write the data to the file
            WriteLog(CreateEntry(ex, details, appendToExisting, severity, timeStamp), fileName, appendToExisting);
            mutex.ReleaseMutex();
        }

        /// <summary>
        /// Creates a log file and includes information contained in <paramref name="ex"/>.
        /// </summary>
        /// <param name="details">Optional additional details that can be added to the Log Entry</param>
        /// <param name="logFolder">Log Output Folder. If none is supplied, the default location is [User]\ApplicationData\[ApplicationName]\Logs\</param>
        /// <param name="appendToExisting">When <see langword="true"/>, the logger will append the log to an existing log file with the current Date Value.</param>
        /// <param name="severity">The severity of the issue being logged.</param>
        public static void CreateLogFile(string? details = null, string? logFolder=null, bool appendToExisting = true, LogSeverity severity = LogSeverity.Info)
        {
            CreateLogFile(null, details, logFolder, appendToExisting, severity);
        }

        /// <summary>
        /// Creates the Log Entry.
        /// </summary>
        /// <param name="ex">Optional Exception to include in the output.</param>
        /// <param name="details">Optional Additinal Details to include in the output.</param>
        /// <param name="appendToExisting">When <see langword="true"/>, adds a horizontal line as a separator.</param>
        /// <param name="severity">The Severity of the entry.</param>
        /// <param name="timeStamp">The Time of the Entry.</param>
        /// <returns></returns>
        private static string CreateEntry(Exception? ex, string? details, bool appendToExisting, LogSeverity severity, DateTime timeStamp)
        {
            StringBuilder sb = new();
            sb.AppendLine(timeStamp.ToString("MM / dd / yy H: mm:ss"));
            sb.AppendLine("Log Severity: " + severity.ToString());
            if (ex is not null)
            {
                sb.AppendLine("Exception Details: ");
                sb.AppendLine(ex.ToString());
            }

            if (!string.IsNullOrWhiteSpace(details))
            {
                sb.AppendLine("Details: ");
                sb.AppendLine(details);
            }

            if (appendToExisting)
            {
                sb.AppendLine();
                sb.AppendLine("------------------------------------------------------------------------------------------------------------------------");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Writes a Log Entry and overwrites all information if an existing file with the same name exists.
        /// </summary>
        /// <param name="entry">The text for the Log Entry.</param>
        /// <param name="logFile">The Target File to write the entry to.</param>
        private static void WriteLog(string entry, string fileName, bool append)
        {
            if (append)
                File.AppendAllText(fileName, entry);
            else
                File.WriteAllText(fileName, entry);
        }
    }

    /// <summary>
    /// Different levels of severity for a Log Entry.
    /// </summary>
    public enum LogSeverity
    {
        Info,
        Warning,
        Error,
    }
}
