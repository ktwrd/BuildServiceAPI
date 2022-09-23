using System.Text.Json;

namespace BuildServiceAPI
{
    public static class ServerStringResponse
    {
        public static string InvalidCredential = "Invalid Credential";
        public static string AccountNotFound(string username)
            => $"Could not find account with username of \"{username}\"";

        public static string InvalidParameter(string parameterName)
            => $"Invalid parameter \"{parameterName}\"";
        public static string InvalidBody => "Invalid Body";

        public static string ExpectedValueOnProperty(string propertyName, object expectedValue, object recievedValue)
            => $"Expected {propertyName} to be \"{JsonSerializer.Serialize(expectedValue)}\" but got \"{JsonSerializer.Serialize(recievedValue)}\" instead";
    }
}
