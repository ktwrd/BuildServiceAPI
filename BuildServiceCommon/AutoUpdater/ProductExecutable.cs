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

        #region bSerializable
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
        #endregion

        #region bFirebaseSerializable
        public Task FromFirebase(DocumentSnapshot document, VoidDelegate completeIncrement)
        {
            this.UID = document.Reference.Id;
            this.Linux = FirebaseHelper.ParseString(document, "Linux");
            this.Windows = FirebaseHelper.ParseString(document, "Windows");
            completeIncrement();
            return Task.CompletedTask;
        }
        public async Task ToFirebase(DocumentReference document, VoidDelegate completeIncrement)
        {
            Dictionary<string, object> content = new Dictionary<string, object>()
            {
                { "Linux", Linux },
                { "Windows", Windows }
            };
            await document.SetAsync(content);
            completeIncrement();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
        #endregion
    }
}
