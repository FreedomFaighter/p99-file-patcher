using Microsoft.VisualStudio.TestTools.UnitTesting;
using p99FileUpdater;

namespace p99_file_updater_NUnit_Test
{
    [TestClass]
    public class HelloWorldViewModelTests
    {
        p99FileDownloaderViewModel hw;

        [TestInitialize]
        public void initializeTest()
        {
            hw = new p99FileDownloaderViewModel();
        }
        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestHelloWorldMessageProperty()
        {
            Assert.AreEqual(hw.MessageBox, string.Empty);
            const string msg = "Test String";
            hw.MessageBox = msg;
            Assert.AreEqual(hw.MessageBox, msg);
            Assert.AreNotEqual(hw.MessageBox, null);
        }

        [TestCategory("MessageDisplayedTests")]
        [TestMethod]
        public void TestHelloWorldButtonPropertyAreEqual()
        {
            hw.MessageDisplayed = false;
            Assert.AreEqual(hw.MessageDisplayed, false);
            hw.MessageDisplayed = true;
            Assert.AreEqual(hw.MessageDisplayed, true);
        }
        [TestCategory("MessageDisplayedTests")]
        [TestMethod]
        public void TestHelloWorldButtonPropertyAreNotEqual()
        {
            hw.MessageDisplayed = true;
            Assert.AreNotEqual(hw.MessageDisplayed, false);
            hw.MessageDisplayed = false;
            Assert.AreNotEqual(hw.MessageDisplayed, true);
        }
    }
}
