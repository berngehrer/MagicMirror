using System;
using System.Net.Http;
using System.Threading;

namespace MagicMirror.Web
{
    public sealed class MessageHandler
    {
        public MessageHandler()
        {
        }
        public MessageHandler(string url)
            : this( new Uri(url) )
        {
        }
        public MessageHandler(Uri url)
        {
            Address = url;
        }

        public Uri Address { get; set; }
        public string MimeString { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public CancellationToken? CancelToken { get; set; }
    }
}
