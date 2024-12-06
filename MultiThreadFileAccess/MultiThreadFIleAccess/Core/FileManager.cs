using MultiThreadFileAccess.Helper;
using MultiThreadFileAccess.Interfaces;


namespace MultiThreadFileAccess.Core
{
    public class FileManager
    {
        
        internal DirectoryManager directoryManager;
        internal IFileWriter fileWriter;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootDirectory">Root directory definition</param>
        /// <param name="logDirectory">Logs folder definition</param>
        /// <param name="fileName">Output file name</param>
        public FileManager(string rootDirectory, string logDirectory, string fileName)
        {            
            directoryManager = new DirectoryManager(Path.Combine(rootDirectory, logDirectory));
            fileWriter = new FileWriter(Path.Combine(rootDirectory, logDirectory, fileName));
        }

        /// <summary>
        /// Method used by thread to write to file 10 lines
        /// </summary>
        /// <returns>If success</returns>
        public bool WriteToMultipleLinesToFile()
        {
            try
            {
                // Each thread writes 10 lines to the same file
                for (int i = 1; i < 11; i++)
                {
                    // Get the current thread ID
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    WriteToFile(threadId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FileManager: Error occured in WriteToFileWithSync when writing to file: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method that checks if directory exists if not creates it and writes the first line
        /// </summary>
        public void InitializeFile()
        {
            directoryManager.EnsureDirectoryExists();
            WriteInitialLineToFile();
        }
        
        /// <summary>
        /// Method to write initial lile also overwrites the file if exists
        /// </summary>
        private void WriteInitialLineToFile()
        {
            try
            {
                string timestamp = DateTime.Now.ToString(Constants.TimestampFormat);
                string line = $"0, 0, {timestamp}\n\n";
                fileWriter.WriteFirstLine(line);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FileManager: Write initial line to file error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Writes to file 
        /// </summary>
        /// <param name="threadId"></param>        
        private void WriteToFile(int threadId)
        {
            try
            {                
                fileWriter.WriteLine(threadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FileManager: Error occured in WriteToFile when writing to file: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
