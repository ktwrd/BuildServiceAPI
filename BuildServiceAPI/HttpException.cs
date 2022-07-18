namespace BuildServiceAPI
{
    public class HttpException
    {
        public HttpException(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; private set; }
        public string Message { get; private set; }
        public readonly bool Error = true;
    }
}
