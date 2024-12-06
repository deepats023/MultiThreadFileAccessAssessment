using MultiThreadFileAccess.Core;
using MultiThreadFileAccess.Helper;

class Program
{
    /// <summary>
    /// Program to create 10 threads to writo to file 10 times and then exit
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {        
        string rootDirectory;
        var tasks = new Task[Constants.ThreadCount];

        Console.WriteLine("Started the MultiThreadFileAccessApp");

        //Get root directory based on environment type(Docker or Windows)
        rootDirectory = GetRootDirectoryBasedOnEvironment();

        try
        {
            // Initialize file helper
            var fileOperationsManager = new FileManager(rootDirectory, Constants.LogDirectory, Constants.FilePath);
            fileOperationsManager.InitializeFile();

            Parallel.For(0, Constants.ThreadCount, i =>
            {
                WriteToMultipleLinesToFile(fileOperationsManager);
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Main: File initialization and parallel thread execution step error: {ex.Message}\n{ex.StackTrace}");
        }

        Console.WriteLine("All threads have completed writing to file.");
        Console.ReadLine();
    }


    /// <summary>
    /// Enables the application to work with correct directory structure for Dockers or Windows environment
    /// </summary>
    /// <returns>Directory path</returns>
    private static string GetRootDirectoryBasedOnEvironment()
    {
        //Get environment variable for type of environment
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        return environment == "DOCKER" ? Constants.DockerRootDir : Constants.WindowsRootDir;
    }

    /// <summary>
    /// Writes to file using FileOperationsManager
    /// </summary>
    /// <param name="FileOperationsManager"></param>
    private static void WriteToMultipleLinesToFile(FileManager fileOperationsManager)
    {
        try
        {
            fileOperationsManager.WriteToMultipleLinesToFile();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Main: File write step execution error: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
