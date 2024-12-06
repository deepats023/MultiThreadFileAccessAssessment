namespace MultiThreadFileAccess.Helper
{
    public static class Constants
    {
        //Thread count
        public const int ThreadCount = 10;

        //Timestamp format for logging in file
        public const string TimestampFormat = "HH:mm:ss.fff";

        //Log directory name
        public const string LogDirectory = "log";

        //Output file name
        public const string FilePath = "out.txt";

        //Docker root directory path
        public const string DockerRootDir = "/log";

        //Windows root directory path
        public const string WindowsRootDir = "c:/junk";
    }
}
