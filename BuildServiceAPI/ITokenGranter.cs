using System.Net.Http;

namespace BuildServiceAPI
{
    public interface ITokenGranter
    {
        public HttpClient httpClient { get; }
        public bool Grant(string username, string password);
    }
}
