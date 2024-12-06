using MultiThreadFileAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadFileAccess.Tests.Core
{
    [TestClass]
    public class FileWriterTests
    {
        private string testFilePath="";

        [TestInitialize]
        public void Setup()
        {
            // Initialize the test file path before each test
            testFilePath = Path.Combine(Path.GetTempPath(), "testfile.txt");

            // Ensure the file is clean before starting tests
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up after the test by deleting the file
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        /// <summary>
        /// Writes first line to file
        /// </summary>
        [TestMethod]
        public void ItShouldWriteFirstLineToFile()
        {
            // Arrange
            var writer = new FileWriter(testFilePath);
            string expectedLine = "First line";

            // Act
            writer.WriteFirstLine(expectedLine);

            // Assert
            string fileContent = File.ReadAllText(testFilePath);
            Assert.AreEqual(expectedLine, fileContent);
        }

        /// <summary>
        /// Adds more lines if already exists
        /// </summary>
        [TestMethod]
        public void ItShouldAppendToExistingLinesInFile()
        {
            // Arrange
            var writer = new FileWriter(testFilePath);
            string firstLine = "First line\n";            

            writer.WriteFirstLine(firstLine);  // Write the first line

            // Act
            writer.WriteLine(2);  // Append the second line

            // Assert
            string[] lines = File.ReadAllLines(testFilePath);
            //Count higher as one blank line in between 2 written lines
            Assert.AreEqual(3, lines.Length);
        }

        /// <summary>
        /// Validates that with multiple tasks file still has correct data
        /// </summary>
        [TestMethod]
        public void ItShouldWriteConcurrentlyWithoutCorruption()
        {
            // Arrange
            var writer = new FileWriter(testFilePath);
            string firstLine = "First line\n";
            
            writer.WriteFirstLine(firstLine);  // Write the first line

            // Act
            Task task1 = Task.Run(() => writer.WriteLine(1)); // Concurrent write 1
            Task task2 = Task.Run(() => writer.WriteLine(2));  // Concurrent write 2

            Task.WhenAll(task1, task2).Wait();  // Wait for both tasks to complete

            // Assert
            string[] lines = File.ReadAllLines(testFilePath);
            //Count higher as one blank line in between 2 written lines
            Assert.AreEqual(5, lines.Length);
        }

        /// <summary>
        /// If file exists it should be overwritten when first line is written
        /// </summary>
        [TestMethod]
        public void ItShouldOverwriteExistingFile()
        {
            // Arrange
            var writer = new FileWriter(testFilePath);
            string initialLine = "Initial Line\n";
            string overwriteLine = "Overwritten Line";

            writer.WriteFirstLine(initialLine);  // Write initial line
            writer.WriteFirstLine(overwriteLine); // Overwrite the file with new content

            // Act
            string fileContent = File.ReadAllText(testFilePath);

            // Assert
            Assert.AreEqual(overwriteLine, fileContent);  // Ensure the file contains the overwrite line
        }
    }
}
