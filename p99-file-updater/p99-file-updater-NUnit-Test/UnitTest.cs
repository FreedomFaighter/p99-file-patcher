using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorld;

namespace Hello_World_NUnit_Test
{
    [TestClass]
    public class HelloWorldViewModelTests
    {
        HelloWorldViewModel hw;

        [TestInitialize]
        public void initializeTest()
        {
            hw = new HelloWorldViewModel();
        }
        [TestCategory("MessageStringTests")]
        [TestMethod]
        public void TestHelloWorldMessageProperty()
        {
            Assert.AreEqual(hw.HelloWorldMessage, string.Empty);
            const string msg = "Test String";
            hw.HelloWorldMessage = msg;
            Assert.AreEqual(hw.HelloWorldMessage, msg);
            Assert.AreNotEqual(hw.HelloWorldMessage, null);
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
