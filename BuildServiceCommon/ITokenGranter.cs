using System.Net.Http;

namespace BuildServiceCommon
{
    public interface ITokenGranter
    {
        bool Grant(string username, string password);
    }
}
