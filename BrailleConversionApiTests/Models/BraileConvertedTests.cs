using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrailleConversionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrailleConversionApi.Models.Tests
{
    [TestClass()]
    public class BraileConvertedTests
    {
        [TestMethod()]
        public void BraileConvertedTest()
        {
            String Letter = "hello";

            BraileConverted letter = new BraileConverted(Letter);


            Assert.AreEqual("hello", letter.CovertedBrailleLetter);
        }

        [TestMethod()]
        public void BraileConvertedTest1()
        {


        }
    }
}