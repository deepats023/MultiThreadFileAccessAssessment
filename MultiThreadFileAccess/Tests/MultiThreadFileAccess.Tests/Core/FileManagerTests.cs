using Moq;
using MultiThreadFileAccess.Core;
using MultiThreadFileAccess.Helper;
using MultiThreadFileAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadFileAccess.Tests.Core
{
    [TestClass]
    public class FileManagerTests
    {
        private string? rootDirectory;
        private string? logDirectory;
        private string? fileName;
        private Mock<IFileWriter> fileWriterMock = new Mock<IFileWriter>();
        private Mock<DirectoryManager>? directoryManagerMock;
        private FileManager? fileManager;

        [TestInitialize]
        public void Setup()
        {
            // Set up test data
            rootDirectory = Path.GetTempPath();
            logDirectory = "TestLogs";
            fileName = "testfile.txt";

            // Mock the IFileWriter and DirectoryManager dependencies
            fileWriterMock = new Mock<IFileWriter>();
            directoryManagerMock = new Mock<DirectoryManager>(Path.Combine(rootDirectory, logDirectory));

            // Create FileManager instance with mocked dependencies
            fileManager = new FileManager(rootDirectory, logDirectory, fileName)
            {
                // Set mock objects in the FileManager
                directoryManager = directoryManagerMock.Object,
                fileWriter = fileWriterMock.Object
            };
        }

        /// <summary>
        /// Validates that file write called correct number of times
        /// </summary>
        [TestMethod]
        public void ItShouldValidateThatFileWriteIsCallSameAsNumberOfLines()
        {
            // Arrange
            fileWriterMock.Setup(fw => fw.WriteLine(It.IsAny<int>())).Verifiable();

            // Act
            var result = fileManager!.WriteToMultipleLinesToFile();

            // Assert
            Assert.IsTrue(result);
            fileWriterMock.Verify(fw => fw.WriteLine(It.IsAny<int>()), Times.Exactly(10));
        }

        [TestMethod]
        public void ItWriteToMultipleLinesToFileReturnsFalseOnFailure()
        {
            // Arrange
            fileWriterMock.Setup(x => x.WriteLine(It.IsAny<int>())).Throws<Exception>();

            // Act
            bool result = fileManager!.WriteToMultipleLinesToFile();

            // Assert
            Assert.IsFalse(result, "WriteToMultipleLinesToFile should return false if an exception is thrown during WriteLine.");
        }

        [TestMethod]
        public void ItShouldInititializeConstructor()
        {
            // Arrange & Act
            FileManager fm = new FileManager("rootDir", "logDir", "file.txt");

            // Assert
            Assert.IsNotNull(fm.directoryManager, "DirectoryManager should be initialized.");
            Assert.IsNotNull(fm.fileWriter, "FileWriter should be initialized.");
        }

    }
}
