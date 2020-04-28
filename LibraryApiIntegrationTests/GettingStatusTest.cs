using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingStatusTest : IClassFixture<WebTestFixture>
    {
        private readonly HttpClient Client;

        public GettingStatusTest(WebTestFixture factory)
        {
            this.Client = factory.CreateClient();
        }

        [Fact]
        public async Task WeGetAnOkStatusCode()
        {
            var response = await Client.GetAsync("/status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WeGetSomeJsonDataBack()
        {
            var response = await Client.GetAsync("/status");
            var contentType = response.Content.Headers.ContentType.MediaType;
            Assert.Equal("application/json", contentType);
        }

        [Fact]
        public async Task ReturnsProperResponse()
        {
            var response = await Client.GetAsync("/status");
            var content = await response.Content.ReadAsAsync<StatusResponseTest>();

            Assert.Equal("This is great stuff", content.Message);
            Assert.Equal("Joe Blow", content.CheckedBy);
            Assert.Equal(new DateTime(1969, 4, 20, 23,59,59), content.WhenLastChecked);
        }
    }

    public class StatusResponseTest
    {
        public string Message { get; set; }
        public string CheckedBy { get; set; }
        public DateTime WhenLastChecked { get; set; }
    }
}
