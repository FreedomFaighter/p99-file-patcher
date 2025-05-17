using Microsoft.VisualStudio.TestTools.UnitTesting;
using p99FileUpdater;
using System;

namespace p99_file_updater_Test
{
    [TestClass]
    public class p99ViewModelTests
    {
        p99FileDownloaderViewModel hw;

        [TestInitialize]
        public void InitializeTest()
        {
            hw = new p99FileDownloaderViewModel();
        }

        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestMessageEmptyOnCreateProperty()
        {
            Assert.AreEqual(hw.MessageBox, string.Empty);
        }

        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestMessageStringPositive() { 
            const string msg = "Test String";
            hw.MessageBox = msg;
            Assert.AreEqual(msg, hw.MessageBox);
        }

        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestMessageStringNullNegative()
        {
            Assert.IsNotNull(hw.MessageBox);
        }

        [TestCategory("OperationEnabledTests")]
        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestButtonMessageDisplayedPropertyAreNotEqual(bool operationEnabled)
        {
            hw.OperationEnabled = operationEnabled;
            Assert.AreNotEqual(!operationEnabled, hw.OperationEnabled);
        }

        [TestMethod]
        public void TestOverrideChecksumValidationTrue()
        {
            const bool expected = true;
            hw.OverrideChecksumValidation = expected;
            Assert.AreEqual(expected, hw.OverrideChecksumValidation);
        }

        [TestMethod]
        public void TestOverrideChecksumValidationFalse()
        {
            const bool expected = false;
            hw.OverrideChecksumValidation = expected;
            Assert.AreEqual (expected, hw.OverrideChecksumValidation);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("https://project1999.com/latest.zip")]
        public void TestDownloadAddress(String uriPath)
        {
            Uri uri = new Uri(uriPath);
            hw.DownloadAddress = new Uri(uriPath);
            Assert.AreEqual(uri, hw.DownloadAddress);
        }
    }
}
