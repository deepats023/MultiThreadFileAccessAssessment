using Moq;
using MultiThreadFileAccess.Core;
using System;
using System.IO;

namespace MultiThreadFileAccess.Tests.Core
{
    [TestClass]
    public class DirectoryManagerTests
    {        
        private readonly string testLogDirectory = "TestDirLogs";

        private DirectoryManager? _directoryManager;
        

        [TestInitialize]
        public void TestInitialize()
        {
            if (Directory.Exists(Path.Combine(Path.GetTempPath(), testLogDirectory)))
            {
                Directory.Delete(Path.Combine(Path.GetTempPath(), testLogDirectory));
            }
           
        }

        //Cleanup between tests
        [TestCleanup]
        public void TestCleanup()
        {
            string logDirectoryPath = Path.Combine(Path.GetTempPath(), testLogDirectory);
            if (Directory.Exists(logDirectoryPath) && !Directory.EnumerateFileSystemEntries(logDirectoryPath).Any())
            {
                Directory.Delete(logDirectoryPath);
            }
        }

        /// <summary>
        /// Test to validate that directory is created successfully
        /// </summary>
        [TestMethod]
        public void ItShouldCreateDirectory()
        {
            //Arrange
            _directoryManager = new DirectoryManager(Path.Combine(Path.GetTempPath(), testLogDirectory));

            //Act
            _directoryManager.EnsureDirectoryExists();

            // Assert: Check if directory is created
            Assert.IsTrue(Directory.Exists(Path.Combine(Path.GetTempPath(), testLogDirectory)));            
        }

        /// <summary>
        /// The program should work fine even if directory exists on the location and no issues
        /// </summary>
        [TestMethod]
        public void ItShouldWorkWhenDirectoryExists()
        {
            //Arrange
            var logDirectory = Path.Combine(Path.GetTempPath(), testLogDirectory);
            Directory.CreateDirectory(logDirectory);
            _directoryManager = new DirectoryManager(Path.Combine(Path.GetTempPath(), testLogDirectory));

            // Act
            _directoryManager.EnsureDirectoryExists();

            // Assert: Check if directory is created
            Assert.IsTrue(Directory.Exists(Path.Combine(Path.GetTempPath(), testLogDirectory)));
        }

    }
}
