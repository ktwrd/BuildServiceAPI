using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;
using Minalyze.Shared.Helpers;

namespace BuildServiceAPI
{
    public class PublishedRelease : bSerializable
    {
        public string CommitHash { get; set; }
        public long Timestamp { get; set; }
        public ReleaseInfo Release { get; set; }
        public PublishedReleaseFile[] Files = Array.Empty<PublishedReleaseFile>();

        public void ReadFromStream(SerializationReader sr)
        {
            CommitHash = sr.ReadString();
            Timestamp = sr.ReadInt64();
            Release = (ReleaseInfo)sr.ReadObject();
            Files = sr.ReadBList<PublishedReleaseFile>().ToArray();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(CommitHash);
            sw.Write(Timestamp);
            sw.WriteObject(Release);
            sw.Write<PublishedReleaseFile>(new List<PublishedReleaseFile>(Files));
        }
    }
    public class PublishedReleaseFile : bSerializable
    {
        public string Location;
        public string CommitHash;
        public FilePlatform Platform = FilePlatform.Any;
        public FileType Type = FileType.Other;
        public void ReadFromStream(SerializationReader sr)
        {
            Location = sr.ReadString();
            CommitHash = sr.ReadString();
            Platform = (FilePlatform)sr.ReadInt32();
            Type = (FileType)sr.ReadInt32();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(Location);
            sw.Write(CommitHash);
            sw.Write(Convert.ToInt32(Platform));
            sw.Write(Convert.ToInt32(Type));
        }
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
