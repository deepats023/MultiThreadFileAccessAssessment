using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadFileAccess.Core
{
    /// <summary>
    /// Directory operations handled here
    /// </summary>
    public class DirectoryManager
    {
        private readonly string logDirectory; 

        public DirectoryManager(string logDirectory)
        {
            this.logDirectory = logDirectory;
        }

        /// <summary>
        /// Handles directory creation
        /// </summary>
        public void EnsureDirectoryExists()
        {
            if (!Directory.Exists(logDirectory))
            {
                try
                {
                    Directory.CreateDirectory(logDirectory);
                    Console.WriteLine($"Directory created at {logDirectory}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when creating directory: {ex.Message}");
                    throw;
                }
            }
        }
    }

}
