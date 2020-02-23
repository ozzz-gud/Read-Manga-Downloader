using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.Tests
{
    [TestClass()]
    public class DownloaderTests
    {
        [TestMethod()]
        public void GetChaptersNumberFromTemplateTest()
        {
            // arrange
            var template = "1-5 15 16-18";
            var ParsedTemplate = new List<int>() { 1, 2, 3, 4, 5, 15, 16, 17, 18 };
            // act
            var ParsedRezult = Downloader.ParseTemplate(template);
            // assert
            CollectionAssert.AreEqual(ParsedTemplate, ParsedRezult);
        }
    }
}