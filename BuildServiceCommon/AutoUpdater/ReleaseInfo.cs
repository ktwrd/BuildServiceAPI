﻿using Google.Cloud.Firestore;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuildServiceCommon.AutoUpdater
{
    public enum ReleaseType
    {
        Invalid = -1,
        Stable,
        Beta,
        Nightly,
        Other
    }
    public interface IReleaseInfo
    {
        string UID { get; }
        string version { get; set; }
        string name { get; set; }
        string productName { get; set; }
        string appID { get; set; }
        long timestamp { get; set; }
        long envtimestamp { get; set; }
        string remoteLocation { get; set; }
        string commitHash { get; set; }
        string commitHashShort { get; set; }
        ReleaseType releaseType { get; set; }
        Dictionary<string, string> files { get; set; }
        Dictionary<string, string> executable { get; set; }
        string[] groupWhitelist { get; set; }
        string[] groupBlacklist { get; set; }
    }
    [Serializable]
    public class ReleaseInfo : IReleaseInfo, bSerializable
    {
        public string UID { get; private set; }
        public string version { get; set; }
        public string name { get; set; }
        public string productName { get; set; }
        public string appID { get; set; }
        public long timestamp { get; set; }
        public long envtimestamp { get; set; }
        public string remoteLocation { get; set; }
        public string commitHash { get; set; }
        public string commitHashShort { get; set; }
        public ReleaseType releaseType { get; set; }
        public Dictionary<string, string> files { get; set; }
        public Dictionary<string, string> executable { get; set; }
        public string[] groupWhitelist { get; set; }
        public string[] groupBlacklist { get; set; }
/*
        #region bFirebaseSerializable
        public Task FromFirebase(DocumentSnapshot document, VoidDelegate completeIncrement)
        {
            this.UID = document.Reference.Id;

            this.version = FirebaseHelper.ParseString(document, "version");
            this.name = FirebaseHelper.ParseString(document, "name");
            this.productName = FirebaseHelper.ParseString(document, "productName");
            this.appID = FirebaseHelper.ParseString(document, "appID");
            this.timestamp = FirebaseHelper.Parse<long>(document, "timestamp", 0);
            this.envtimestamp = FirebaseHelper.Parse<long>(document, "envtimestamp", 0);
            this.remoteLocation = FirebaseHelper.ParseString(document, "remoteLocation");
            this.commitHash = FirebaseHelper.ParseString(document, "commitHash");
            this.commitHashShort = FirebaseHelper.ParseString(document, "commitHashShort");
            this.releaseType = FirebaseHelper.Parse<ReleaseType>(document, "releaseType", ReleaseType.Other);
            this.files = FirebaseHelper.Parse<Dictionary<string, string>>(document, "files", new Dictionary<string, string>());
            this.executable = FirebaseHelper.Parse<Dictionary<string, string>>(document, "executable", new Dictionary<string, string>());
            completeIncrement();
            return Task.CompletedTask;
        }
        public async Task ToFirebase(DocumentReference document, VoidDelegate completeIncrement)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "version", version },
                { "name", name },
                { "productName", productName },
                { "appID", appID },
                { "timestamp", timestamp },
                { "envtimestamp", envtimestamp },
                { "remoteLocation", remoteLocation },
                { "commitHash", commitHash },
                { "commitHashShort", commitHashShort },
                { "releaseType", releaseType },
                { "files", files },
                { "executable", executable }
            };
            await document.SetAsync(data);
            completeIncrement();
        }
        public DocumentReference GetFirebaseDocumentReference(FirestoreDb database) => database.Document(FirebaseHelper.FirebaseCollection[this.GetType()] + "/" + UID);
        #endregion
*/
        public static ReleaseInfo Blank()
        {
            return new ReleaseInfo();
        }
        public ReleaseInfo()
        {
            UID = GeneralHelper.GenerateUID();
            version = @"";
            name = @"";
            productName = @"";
            appID = @"";
            timestamp = 0;
            envtimestamp = 0;
            remoteLocation = @"";
            commitHash = @"";
            commitHashShort = @"";
            releaseType = ReleaseType.Other;
            files = new Dictionary<string, string>();
            executable = new Dictionary<string, string>();
            groupWhitelist = Array.Empty<string>();
            groupBlacklist = Array.Empty<string>();
        }

        #region bSerializable
        public void ReadFromStream(SerializationReader sr)
        {
            version = sr.ReadString();
            name = sr.ReadString();
            productName = sr.ReadString();
            appID = sr.ReadString();
            timestamp = sr.ReadInt64();
            envtimestamp = sr.ReadInt64();
            remoteLocation = sr.ReadString();
            commitHash = sr.ReadString();
            commitHashShort = sr.ReadString();
            releaseType = (ReleaseType)sr.ReadInt32();
            files = (Dictionary<string, string>)sr.ReadDictionary<string, string>();
            executable = (Dictionary<string, string>)sr.ReadDictionary<string, string>();
            UID = sr.ReadString();
        }
        public void WriteToStream(SerializationWriter sw)
        {
            sw.Write(version);
            sw.Write(name);
            sw.Write(productName);
            sw.Write(appID);
            sw.Write(timestamp);
            sw.Write(envtimestamp);
            sw.Write(remoteLocation);
            sw.Write(commitHash);
            sw.Write(commitHashShort);
            sw.Write(Convert.ToInt32(releaseType));
            sw.Write(files);
            sw.Write(executable);
            sw.Write(UID);
        }
        #endregion
    }
}
