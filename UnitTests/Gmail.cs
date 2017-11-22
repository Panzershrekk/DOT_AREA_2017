using Module;
using Xunit;

namespace UnitTests
{
    public class GmailTest
    {
        private ModuleGmail module { get; set; }
        
        [Fact]
        public void TestSendMessage()
        {
            module = new ModuleGmail();
            var test = module.GmailSendMessage("grattepanche.robin@gmail.com", "test", "hello world!");
            Assert.Equal(test, true);
        }
    }
}