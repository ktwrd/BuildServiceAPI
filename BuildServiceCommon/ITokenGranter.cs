using System.Net.Http;

namespace BuildServiceAPI
{
    public interface ITokenGranter
    {
        HttpClient httpClient { get; }
        bool Grant(string username, string password);
    }
}
