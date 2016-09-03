namespace SimpleInjectorMySqlBugHunt.Tests
{
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Owin.Testing;

    using Xunit;

    public class ValuesControllerTests
    {
        public void TestRepsonse()
        {
        }

        private const string SaveThirdStepUrl = "/api/values/getvalues";

        [Fact]
        public async Task<int> TestFailing()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await server.HttpClient.GetAsync(SaveThirdStepUrl);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var sampleList = await response.Content.ReadAsAsync<string[]>();
                Assert.Equal(2, sampleList.Length);
            }

            return 0;
        }

        [Fact]
        public async Task<int> TestWithWorkaround()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var t = ConfigurationManager.GetSection("a");
                var response = await server.HttpClient.GetAsync(SaveThirdStepUrl);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var sampleList = await response.Content.ReadAsAsync<string[]>();
                Assert.Equal(2, sampleList.Length);
            }

            return 0;
        }
    }
}
