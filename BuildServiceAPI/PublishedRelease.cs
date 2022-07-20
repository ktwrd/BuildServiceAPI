using BuildServiceCommon.AutoUpdater;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.Helpers;
using kate.shared.Helpers;

namespace BuildServiceAPI
{
    [Serializable]
    public class PublishedRelease : bSerializable, bFirebaseSerializable
    {
        public string UID = GeneralHelper.GenerateUID();
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
        public void ToFirebase(DocumentReference document)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "CommitHash", CommitHash },
                { "Timestamp", Timestamp },
                { "Release", Release.GetFirebaseDocumentReference(document.Database) }
            };
            var fileList = new List<DocumentReference>();
            foreach (var file in Files)
            {
                var refr = file.GetFirebaseDocumentReference(document.Database);
                file.ToFirebase(refr);
                fileList.Add(refr);
            }
            data.Add("Files", fileList.ToArray());
            document.SetAsync(data).Wait();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
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
        public string UID = GeneralHelper.GenerateUID();
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
        public void ToFirebase(DocumentReference document)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "Location", Location },
                { "CommitHash", CommitHash },
                { "Platform", Platform },
                { "Type", Type }
            };
            document.SetAsync(data).Wait();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
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
