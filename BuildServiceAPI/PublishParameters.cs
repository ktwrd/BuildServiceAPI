using Minalyze.Shared.AutoUpdater;

namespace BuildServiceAPI
{
    public class PublishParameters
    {
        public string token { get; set; }
        public string organization { get; set; }
        public string product { get; set; }
        public string branch { get; set; }
        public long timestamp { get; set; }
        public ReleaseInfo releaseInfo { get; set; }
        public ManagedUploadSendData[] files = Array.Empty<ManagedUploadSendData>();
    }

    public class ManagedUploadSendData
    {
        public string Location;
        public string ETag;
        public string Bucket;
        public string Key;
    }
}
