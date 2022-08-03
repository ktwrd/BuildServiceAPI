using System.Net.Http;

namespace BuildServiceCommon
{
    public interface ITokenGranter
    {
        HttpClient httpClient { get; }
        bool Grant(string username, string password);
    }
}
