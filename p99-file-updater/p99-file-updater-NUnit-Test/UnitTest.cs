using Microsoft.VisualStudio.TestTools.UnitTesting;
using p99FileUpdater;
using System;

namespace p99_file_updater_NUnit_Test
{
    [TestClass]
    public class p99ViewModelTests
    {
        p99FileDownloaderViewModel hw;

        [TestInitialize]
        public void initializeTest()
        {
            hw = new p99FileDownloaderViewModel();
        }
        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestMessageProperty()
        {
            const string msg = "Test String";
            hw.MessageBox = msg;
            Assert.AreEqual(hw.MessageBox, msg);
        }

        [TestMethod]
        public void TestMessagePropertyEmptyNotNull()
        {
            hw.MessageBox = String.Empty;
            Assert.AreNotEqual(hw.MessageBox, null);
        }

        [TestCategory("MessageDisplayedTests")]
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestButtonMessageDisplayedPropertyAreNotEqual(bool isDisabled)
        {
            hw.OperationEnabled = isDisabled;
            Assert.AreNotEqual(hw.DisableDownloadButton, isDisabled);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestOverrideChecksumValidationTrue(bool checksumOverride)
        {
            hw.OverrideChecksumValidation = checksumOverride;
            Assert.AreEqual(hw.OverrideChecksumValidation, checksumOverride);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("https://project1999.com/latest.zip")]
        public void TestdownloadAddressEmpty(String url)
        {
            Uri uri = new Uri(url);
            hw.DownloadAddress = new Uri(uri.OriginalString);
            Assert.AreEqual(hw.DownloadAddress, uri);
        }
    }
}
