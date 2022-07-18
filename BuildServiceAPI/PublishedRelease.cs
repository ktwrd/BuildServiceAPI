using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;

namespace BuildServiceAPI
{
    public class PublishedRelease
    {
        public string CommitHash { get; set; }
        public long Timestamp { get; set; }
        public ReleaseInfo Release { get; set; }
        public PublishedReleaseFile[] Files = Array.Empty<PublishedReleaseFile>();
    }
    public class PublishedReleaseFile
    {
        public string Location;
        public string CommitHash;
        public FilePlatform Platform = FilePlatform.Any;
        public FileType Type = FileType.Other;
    }
    public enum FileType
    {
        Other,
        Portable,
        Installer
    }
    public enum FilePlatform
    {
        Any,
        Windows,
        Linux
    }
}
