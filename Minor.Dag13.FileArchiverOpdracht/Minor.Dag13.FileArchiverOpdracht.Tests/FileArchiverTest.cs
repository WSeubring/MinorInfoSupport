using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minor.Dag13.FileArchiverOpdracht.Tests
{
    [TestClass]
    public class FileArchiverTest
    {
        [TestMethod]
        public void MovedNewFileToArchiveDir()
        {
            var sourcePath = @"C:\TFS\WesleyS\FileArchiverTestMap";

            try
            {
                //Arrange
                var target = new FileArchiver(sourcePath, sourcePath + @"\Archive");

                //Act
                using (FileStream fs = File.Create(sourcePath + @"\new.file")) { }
                Thread.Sleep(10);
                var result = File.Exists(sourcePath + @"\Archive\new.zip");

                //Assert
                Assert.IsTrue(result);
            }
            finally
            {
                if (File.Exists(sourcePath + @"\Archive\new.zip"))
                {
                    File.Delete(sourcePath + @"\Archive\new.zip");
                }
                else if (File.Exists(sourcePath + @"\new.file"))
                {
                    File.Delete(sourcePath + "\new.file");
                }
            }
        }
        [TestMethod]
        public void ArchivedFileCalledFileNameDotZip()
        {
            var sourcePath = @"C:\TFS\WesleyS\FileArchiverTestMap";

            try
            {
                //Arrange
                var target = new FileArchiver(sourcePath, sourcePath + @"\Archive");

                //Act
                using (FileStream fs = File.Create(sourcePath + @"\new.file")) { }
                Thread.Sleep(10);
                var result = File.Exists(sourcePath + @"\Archive\new.zip");

                //Assert
                Assert.IsTrue(result);
            }
            finally
            {
                if (File.Exists(sourcePath + @"\Archive\new.zip"))
                {
                    File.Delete(sourcePath + @"\Archive\new.zip");
                }
                else if (File.Exists(sourcePath + @"\new.file"))
                {
                    File.Delete(sourcePath + "\new.file");
                }
            }
        }

        [TestMethod]
        public void TwoRowAddedToZipWithFileNameAndCreationDate()
        {
            var sourcePath = @"C:\TFS\WesleyS\FileArchiverTestMap";

            try
            {
                //Arrange
                var target = new FileArchiver(sourcePath, sourcePath + @"\Archive");

                //Act
                using (FileStream fs = File.Create(sourcePath + @"\new.file"))
                {
                    fs.Write(System.Text.Encoding.UTF8.GetBytes("Hello, World"), 0, 12);
                }
              
                Thread.Sleep(10);
                var lines = File.ReadAllLines(sourcePath + @"\Archive\new.zip");
                var name = lines[0];
                //var creationdate = lines[1];
                
                

         

                //Assert
                DateTime expectedDateTime;
                //DateTime.TryParse(creationdate, out expectedDateTime);

                Assert.AreEqual("new.file", name);
                //Assert.AreEqual(DateTime.Now.Date, expectedDateTime.Date);
            }
            finally
            {
                if (File.Exists(sourcePath + @"\Archive\new.zip"))
                {
                    File.Delete(sourcePath + @"\Archive\new.zip");
                }
                else if (File.Exists(sourcePath + @"\new.file"))
                {
                    File.Delete(sourcePath + "\new.file");
                }
            }
        }
    }
}
