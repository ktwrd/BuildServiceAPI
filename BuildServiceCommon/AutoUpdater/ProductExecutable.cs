using Google.Cloud.Firestore;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon.AutoUpdater
{
    [Serializable]
    public class ProductExecutable : bSerializable, bFirebaseSerializable
    {
        public string UID = GeneralHelper.GenerateUID();
        public string Linux = @"";
        public string Windows = @"";

        public void ReadFromStream(SerializationReader sr)
        {
            Linux = sr.ReadString();
            Windows = sr.ReadString();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(Linux);
            sw.Write(Windows);
        }

        public void FromFirebase(DocumentSnapshot document)
        {
            this.UID = document.Reference.Id;
            this.Linux = FirebaseHelper.ParseString(document, "Linux");
            this.Windows = FirebaseHelper.ParseString(document, "Windows");
        }
        public void ToFirebase(DocumentReference document)
        {
            Dictionary<string, object> content = new Dictionary<string, object>()
            {
                { "Linux", Linux },
                { "Windows", Windows }
            };
            document.UpdateAsync(content);
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
    }
}
