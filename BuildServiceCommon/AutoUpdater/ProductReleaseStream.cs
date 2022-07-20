using Google.Cloud.Firestore;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon.AutoUpdater
{
    [Serializable]
    public class ProductReleaseStream : bSerializable, bFirebaseSerializable
    {
        public string UID = GeneralHelper.GenerateUID();

        public string ProductID = @"";

        public string ProductName = @"";
        public string ProductVersion = @"";
        public long ProductExpiryTimestamp = 0;
        public DateTimeOffset ProductExpiryAt = DateTimeOffset.FromUnixTimeMilliseconds(0);

        public string BranchName = @"";
        public long UpdatedTimestamp = 0;
        public DateTimeOffset UpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(0);

        public string RemoteSignature = @"";
        public ProductExecutable Executable = new ProductExecutable();
        public string CommitHash = @"";
        public void FromFirebase(DocumentSnapshot document)
        {
            this.UID = document.Reference.Id;

            this.ProductID = FirebaseHelper.ParseString(document, "ProductID");
            this.ProductName = FirebaseHelper.ParseString(document, "ProductName");
            this.ProductVersion = FirebaseHelper.ParseString(document, "Version");
            this.BranchName = FirebaseHelper.ParseString(document, "BranchName");
            this.RemoteSignature = FirebaseHelper.ParseString(document, "RemoteSignature");
            this.UpdatedTimestamp = FirebaseHelper.Parse<long>(document, "UpdatedTimestamp", 0);
            var dict = document.ToDictionary();
            if (dict["Executable"] != null)
            {
                var dc = (DocumentReference)dict["Executable"];
                Executable = FirebaseHelper.DeserializeDocumentReference<ProductExecutable>(dc);
            }
            this.CommitHash = FirebaseHelper.ParseString(document, "CommitHash");
        }
        public void ToFirebase(DocumentReference document)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "ProductID", ProductID },
                { "ProductName", ProductName },
                { "Version", ProductVersion },
                { "BranchName", BranchName },
                { "RemoteSignature", RemoteSignature },
                { "UpdatedTimestamp", UpdatedTimestamp },
                { "CommitHash", CommitHash }
            };
            var execRef = Executable.GetFirebaseDocumentReference(document.Database);
            Executable.ToFirebase(execRef);
            data.Add("Executable", execRef);
            document.SetAsync(data).Wait();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
        public void ReadFromStream(SerializationReader sr)
        {
            ProductID = sr.ReadString();
            ProductName = sr.ReadString();
            ProductVersion = sr.ReadString();
            ProductExpiryTimestamp = sr.ReadInt64();
            ProductExpiryAt = DateTimeOffset.FromUnixTimeMilliseconds(ProductExpiryTimestamp);

            BranchName = sr.ReadString();
            UpdatedTimestamp = sr.ReadInt64();
            UpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(UpdatedTimestamp);

            RemoteSignature = sr.ReadString();
            Executable = (ProductExecutable)sr.ReadObject();
            CommitHash = sr.ReadString();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(ProductID);
            sw.Write(ProductName);
            sw.Write(ProductVersion);
            sw.Write(ProductExpiryTimestamp);

            sw.Write(BranchName);
            sw.Write(UpdatedTimestamp);

            sw.Write(RemoteSignature);
            sw.WriteObject(Executable);

            sw.Write(CommitHash);
        }
    }
    [Serializable]
    public class ProductRelease : bSerializable, bFirebaseSerializable
    {
        public string UID = GeneralHelper.GenerateUID();
        public string ProductName = @"";
        public string ProductID = @"";
        public ProductReleaseStream[] Streams = Array.Empty<ProductReleaseStream>();

        
        public void FromFirebase(DocumentSnapshot document)
        {
            this.UID = document.Reference.Id;

            this.ProductName = FirebaseHelper.ParseString(document, "ProductName");
            this.ProductID = FirebaseHelper.ParseString(document, "ProductID");

            var dict = document.ToDictionary();
            var streamList = new List<ProductReleaseStream>();
            if (dict.ContainsKey("Streams"))
            {
                foreach (object fz in (List<object>)dict["Streams"])
                {
                    var f = (DocumentReference)fz;
                    var res = FirebaseHelper.DeserializeDocumentReference<ProductReleaseStream>(f);
                    if (res != null)
                        streamList.Add(res);
                }
            }
            this.Streams = streamList.ToArray();
        }
        public void ToFirebase(DocumentReference document)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "ProductName", ProductName },
                { "ProductID", ProductID }
            };
            var refList = new List<DocumentReference>();
            foreach (var stream in Streams)
            {
                var refr = stream.GetFirebaseDocumentReference(document.Database);
                stream.ToFirebase(refr);
                refList.Add(refr);
            }
            data.Add("Streams", refList);
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);

        public void ReadFromStream(SerializationReader sr)
        {
            ProductName = sr.ReadString();
            ProductID = sr.ReadString();
            Streams = ((List<ProductReleaseStream>)sr.ReadBList<ProductReleaseStream>()).ToArray();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(ProductName);
            sw.Write(ProductID);
            sw.Write<ProductReleaseStream>(new List<ProductReleaseStream>(Streams));
        }
    }
}
