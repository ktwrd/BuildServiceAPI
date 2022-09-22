namespace BuildServiceAPI
{
    public static class ServerStringResponse
    {
        public static string InvalidCredential = "Invalid Credential";
        public static string AccountNotFound(string username)
            => $"Could not find account with username of \"{username}\"";
    }
}
