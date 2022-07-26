using BuildServiceCommon.AutoUpdater;
using Google.Cloud.Firestore;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildServiceCommon
{
    [Serializable]
    public class PublishedRelease : bSerializable, bFirebaseSerializable
    {
        public string UID = GeneralHelper.GenerateUID();
        public string CommitHash = "";
        public long Timestamp = 0;
        public ReleaseInfo Release = ReleaseInfo.Blank();
        public PublishedReleaseFile[] Files = Array.Empty<PublishedReleaseFile>();

        public async Task FromFirebase(DocumentSnapshot document, VoidDelegate completeIncrement)
        {
            this.UID = document.Reference.Id;

            this.CommitHash = FirebaseHelper.ParseString(document, "CommitHash");
            this.Timestamp = FirebaseHelper.Parse<long>(document, "Timestamp", 0);
            var fileList = new List<PublishedReleaseFile>();
            var taskList = new List<Task>();
            var dict = document.ToDictionary();
            if (dict.ContainsKey("Files"))
            {
                foreach (object fz in (List<object>)dict["Files"])
                {
                    taskList.Add(new Task(new Action(async delegate
                    {
                        var f = (DocumentReference)fz;
                        var res = await FirebaseHelper.DeserializeDocumentReference<PublishedReleaseFile>(f, completeIncrement);
                        if (res != null)
                            fileList.Add(res);
                        completeIncrement();
                    })));
                }
            }
            foreach (var i in taskList)
                i.Start();
            await Task.WhenAll(taskList);
            Files = fileList.ToArray();
            if (dict.ContainsKey("Release"))
            {
                var r = await FirebaseHelper.DeserializeDocumentReference<ReleaseInfo>((DocumentReference)dict["Release"], completeIncrement);
                Release = r;
                completeIncrement();
            }
            completeIncrement();
        }
        public async Task ToFirebase(DocumentReference document, VoidDelegate completeIncrement)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "CommitHash", CommitHash },
                { "Timestamp", Timestamp },
                { "Release", Release.GetFirebaseDocumentReference(document.Database) }
            };
            var fileList = new List<DocumentReference>();
            var taskList = new List<Task>();
            foreach (var file in Files)
            {
                taskList.Add(new Task(new Action(async delegate
                {
                    var refr = file.GetFirebaseDocumentReference(document.Database);
                    await file.ToFirebase(refr, completeIncrement);
                    fileList.Add(refr);
                    completeIncrement();
                })));
            }
            foreach (var i in taskList)
                i.Start();
            await Task.WhenAll(taskList);
            data.Add("Files", fileList.ToArray());
            await document.SetAsync(data);
            completeIncrement();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
        public void ReadFromStream(SerializationReader sr)
        {
            CommitHash = sr.ReadString();
            Timestamp = sr.ReadInt64();
            Release = (ReleaseInfo)sr.ReadObject();
            Files = new List<PublishedReleaseFile>(sr.ReadBList<PublishedReleaseFile>()).ToArray();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(CommitHash);
            sw.Write(Timestamp);
            sw.WriteObject(Release);
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

        public Task FromFirebase(DocumentSnapshot document, VoidDelegate completeIncrement)
        {
            this.UID = document.Reference.Id;

            this.Location = FirebaseHelper.ParseString(document, "Location");
            this.CommitHash = FirebaseHelper.ParseString(document, "CommitHash");
            this.Platform = FirebaseHelper.Parse<FilePlatform>(document, "Platform", FilePlatform.Any);
            this.Type = FirebaseHelper.Parse<FileType>(document, "Type", FileType.Other);
            completeIncrement();
            return Task.CompletedTask;
        }
        public async Task ToFirebase(DocumentReference document, VoidDelegate completeIncrement)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "Location", Location },
                { "CommitHash", CommitHash },
                { "Platform", Platform },
                { "Type", Type }
            };
            await document.SetAsync(data);
            completeIncrement();
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
