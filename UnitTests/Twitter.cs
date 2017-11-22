using System.IO;
using System.Linq;
using Dapper;
using Module;
using Xunit;

namespace UnitTests
{
    public class Twitter
    {
        [Fact]
        public void TwitterPostWhenError()
        {
            var module = new ModuleTwitter();
            const string test = "";
            var response = module.TwitterPostRequest(test);
            Assert.Equal(response, false);
        }

        [Fact]
        public void TwitterPostWhenNoError()
        {
            var module = new ModuleTwitter();
            const string test = "test";
            var response = module.TwitterPostRequest(test);
            Assert.NotEqual(response, false);
        }

        [Fact]
        public void TwitterGet()
        {
            var module = new ModuleTwitter();

            var response = module.TwitterGetUsername();
            Assert.NotEqual(response, "Error");
        }
    }
}