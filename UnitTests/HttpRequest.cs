using Module;
using Xunit;
using HttpResponse;

namespace UnitTests
{
    public class HttpRequestTest
    {
        
        private HttpRequest Response { get; set; }

        [Fact]
        public void TestOkHttpResponse()
        {
            Response = new HttpRequest();
            string test = "{\"Status\":\"OK\",\"Message\":\"Test unit test\"}";

            Response.ChangeResponse("OK", "Test unit test");
            Assert.Equal(test, Response.ToJson());
        }

        public void TestKoHttpResponse()
        {
            Response = new HttpRequest();
            string test = "{\"Status\":\"KO\",\"Message\":\"Test unit test\"}";

            Response.ChangeResponse("KO", "Test unit test");
            Assert.Equal(test, Response.ToJson());
        }

        public void TestCreateHttpResponse()
        {
            Response = new HttpRequest();
            string test = "{\"Status\":\"KO\",\"Message\":\"Message returned not defined\"}";

            Assert.Equal(test, Response.ToJson());
        }
        
    }
}