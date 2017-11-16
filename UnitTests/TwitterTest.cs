using System.IO;
using System.Linq;
using Dapper;
using Module;
using Xunit;

namespace UnitTests
{
    public class TwitterTest
    {
        [Fact]
        public void TwitterPostWhenError()
        {
            var module = new ModuleTwitter();
            string test = "";
            var response = module.PostRequest(test);
            Assert.Equal(response, "Error");
        }

        [Fact]
        public void TwitterPostWhenNoError()
        {
            var module = new ModuleTwitter();
            string test = "test";
            var response = module.PostRequest(test);
            Assert.NotEqual(response, "Error");
        }

        [Fact]
        public void TwitterGet()
        {
            var module = new ModuleTwitter();

            var response = module.GetRequest();
            Assert.NotEqual(response, "Error");
        }
    }
}