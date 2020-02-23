using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class TitleTests
    {
        [TestMethod()]
        public void TitleTest()
        {
            // arrange
            var expectedCountChapters = 18;
            var expectedCountImages = 6;
            // act
            var title = new Title("https://readmanga.me/imperskaia_nalojnica", false);
            title[0].UpdateChapterInfo();
            // assert
            Assert.AreEqual(expectedCountChapters, title.Count());
            Assert.AreEqual(expectedCountImages, title[0].CountImage);
        }
    }
}