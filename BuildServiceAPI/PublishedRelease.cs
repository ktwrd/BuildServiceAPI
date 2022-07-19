using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;
using Minalyze.Shared.Helpers;

namespace BuildServiceAPI
{
    [Serializable]
    public class PublishedRelease : bSerializable, bFirebaseSerializable
    {
        public string UID = "";
        public string CommitHash = "";
        public long Timestamp = 0;
        public ReleaseInfo Release = ReleaseInfo.Blank();
        public PublishedReleaseFile[] Files = Array.Empty<PublishedReleaseFile>();

        public void FromFirebase(DocumentSnapshot document)
        {
            this.UID = document.Reference.Id;

            this.CommitHash = FirebaseHelper.ParseString(document, "CommitHash");
            this.Timestamp = FirebaseHelper.Parse<long>(document, "Timestamp", 0);
            var fileList = new List<PublishedReleaseFile>();
            var dict = document.ToDictionary();
            if (dict.ContainsKey("Files"))
            {
                foreach (object fz in (List<object>)dict["Files"])
                {
                    var f = (DocumentReference)fz;
                    var res = FirebaseHelper.DeserializeDocumentReference<PublishedReleaseFile>(f);
                    if (res != null)
                        fileList.Add(res);
                }
            }
            Files = fileList.ToArray();
            if (dict.ContainsKey("Release"))
            {
                Release = FirebaseHelper.DeserializeDocumentReference<ReleaseInfo>((DocumentReference)dict["Release"]);
            }
        }
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
            sw.WriteObject(Release == null ? (byte)ObjectType.nullType : Release);
            sw.Write(new List<PublishedReleaseFile>(Files));
        }
    }
    [Serializable]
    public class PublishedReleaseFile : bSerializable, bFirebaseSerializable
    {
        public string UID = "";
        public string Location = "";
        public string CommitHash = "";
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

        public void FromFirebase(DocumentSnapshot document)
        {
            this.UID = document.Reference.Id;

            this.Location = FirebaseHelper.ParseString(document, "Location");
            this.CommitHash = FirebaseHelper.ParseString(document, "CommitHash");
            this.Platform = FirebaseHelper.Parse<FilePlatform>(document, "Platform", FilePlatform.Any);
            this.Type = FirebaseHelper.Parse<FileType>(document, "Type", FileType.Other);
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
