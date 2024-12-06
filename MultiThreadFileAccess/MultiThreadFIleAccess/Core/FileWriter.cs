using MultiThreadFileAccess.Helper;
using MultiThreadFileAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadFileAccess.Core
{
    /// <summary>
    /// This class handles various write operations to the file
    /// </summary>
    public class FileWriter : IFileWriter
    {
        private readonly string filePath;
        private static int lineCount = 0;

        // Lock object for synchronizing file writes
        private static readonly object fileWriteLock = new object();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath"></param>
        public FileWriter(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Writes first line overwriting file if it exists
        /// </summary>
        /// <param name="line"></param>
        public void WriteFirstLine(string line)
        {
            File.WriteAllText(filePath, line);
        }

        /// <summary>
        /// Appends new lines to existing file leveraging lock
        /// </summary>
        /// <param name="line"></param>
        public void WriteLine(int threadId) {
            lock (fileWriteLock)
            {
                //Consistent count needed hence when locking for file is when lineCount should be incremented to be in order
                string timestamp = DateTime.Now.ToString(Constants.TimestampFormat);
                string line = $"{Interlocked.Increment(ref lineCount)}, {threadId}, {timestamp}\n\n";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteAsync(line);
                }
            }
        }
    }
}
