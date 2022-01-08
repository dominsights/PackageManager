using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DgSystems.PackageManagerUnitTests
{
    internal class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage httpResponseMessage;

        public MockHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(httpResponseMessage);
        }
    }
}
