using System.Net;
using System.Net.Http;

namespace DgSystems.PackageManagerUnitTests.Features
{
    public class HttpClientFixture
    {
        public HttpClient HttpClient { get; }

        public HttpClientFixture()
        {
            HttpResponseMessage? httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = downloadContent
            };
            HttpClient = new HttpClient(new MockHttpMessageHandler(httpResponseMessage));
        }

        readonly MultipartContent? downloadContent = new MultipartContent("zip") { new ByteArrayContent(new byte[64]) };
    }
}
