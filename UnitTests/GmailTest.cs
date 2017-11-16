using Module;
using Xunit;

namespace UnitTests
{
    public class GmailTest
    {
        private Module.GmailModule module { get; set; }
        [Fact]
        public void TestSendMessage()
        {
            module = new GmailModule();
            string test = module.SendMessage("grattepanche.robin@gmail.com", "test", "hello world!");
            Assert.Equal(test, "OK");
        }
    }
}