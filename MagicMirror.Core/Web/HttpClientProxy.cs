using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MagicMirror.Web
{
    public sealed class HttpClientProxy : IDisposable
    {
        HttpClient _innerClient;

        public HttpClientProxy()
        {
            _innerClient = CreateClient();
        }

        public HttpClientProxy(string hostAddress)
            : this( new Uri(hostAddress) )
        {
        }

        public HttpClientProxy(Uri hostAddress)
        {
            _innerClient = CreateClient(hostAddress);
        }

        public async Task<string> ReadAsString(Uri url)
        {
            return await ReadAsString(new MessageHandler(url));
        }

        public async Task<string> ReadAsString(MessageHandler message)
        {
            using (var response = await GetResponse(message))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<Stream> ReadAsStream(Uri url)
        {
            return await ReadAsStream(new MessageHandler(url));
        }

        public async Task<Stream> ReadAsStream(MessageHandler message)
        {
            using (var response = await GetResponse(message))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStreamAsync();
            }
        }

        Task<HttpResponseMessage> GetResponse(MessageHandler message)
        {
            var request = new HttpRequestMessage(message.Method, message?.Address);

            if (!string.IsNullOrEmpty(message.MimeString))
            {
                var header = new MediaTypeWithQualityHeaderValue(message.MimeString);
                request.Headers.Accept.Add(header);
            }

            if (message.CancelToken.HasValue) {
                return _innerClient.SendAsync(request, message.CancelToken.Value);
            }
            else {
                return _innerClient.SendAsync(request);
            }
        }

        HttpClient CreateClient(Uri hostAddress = null)
        {
            var httpHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            return new HttpClient(httpHandler, true)
            {
                BaseAddress = hostAddress
            };
        }

        public void Dispose()
        {
            _innerClient?.CancelPendingRequests();
            _innerClient?.Dispose();
        }
    }
}
