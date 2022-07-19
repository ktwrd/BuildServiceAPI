using Google.Cloud.Firestore;
using Minalyze.Shared.AutoUpdater;
using Minalyze.Shared.Helpers;

namespace BuildServiceAPI
{
    public class ContentManager
    {
        public List<ReleaseInfo> ReleaseInfoContent = new();
        public Dictionary<string, ProductRelease> Releases = new();
        public Dictionary<string, PublishedRelease> Published = new();
        internal List<string> LoadedFirebaseAssets = new();
        internal FirestoreDb database;

        internal int DatabaseVersion;

        private readonly string DATABASE_FILENAME = Path.Join(
            Directory.GetCurrentDirectory(),
            @"content.db");

        public ContentManager()
        {
            database = FirestoreDb.Create(@"cloudtesting-3d734");
            LoadFirebase();
        }
        static ContentManager()
        {
            FirebaseHelper.FirebaseCollection.Add(typeof(PublishedRelease), "PublishedRelease");
            FirebaseHelper.FirebaseCollection.Add(typeof(PublishedReleaseFile), "PublishedReleaseFile");
        }

        public void SaveFirebase()
        {
            Console.WriteLine($"[ContentManager->SaveFirebase] Uploading Content to Firebase");
            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var count = 0;
            foreach (var item in ReleaseInfoContent)
            {
                item.ToFirebase(item.GetFirebaseDocumentReference(database));
                count++;
            }
            foreach (var pair in Releases)
            {
                pair.Value.ToFirebase(pair.Value.GetFirebaseDocumentReference(database));
                count++;
                count += pair.Value.Streams.Length * 2;
            }
            foreach (var pair in Published)
            {
                pair.Value.ToFirebase(pair.Value.GetFirebaseDocumentReference(database));
                count++;
                count += pair.Value.Files.Length + 1;
            }
            Console.WriteLine($"[ContentManager->SaveFirebase] Uploaded {count} items in {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTimestamp}ms");
        }
        public void LoadFirebase()
        {
            Console.WriteLine($"[ContentManager->LoadFirebase] Fetching Content from Firebase");
            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var count = 0;
            foreach (var item in ReleaseInfoContent)
            {
                var snapshot = item.GetFirebaseDocumentReference(database).GetSnapshotAsync().Result;
                item.FromFirebase(snapshot);
                count++;
            }
            foreach (var pair in Releases)
            {
                var snapshot = pair.Value.GetFirebaseDocumentReference(database).GetSnapshotAsync().Result;
                pair.Value.FromFirebase(snapshot);
                count++;
                count += pair.Value.Streams.Length * 2;
            }
            foreach (var pair in Published)
            {
                var snapshot = pair.Value.GetFirebaseDocumentReference(database).GetSnapshotAsync().Result;
                pair.Value.FromFirebase(snapshot);
                count++;
                count += pair.Value.Files.Length + 1;
            }

            var newReleaseInfoContent = new List<ReleaseInfo>();
            var newReleases = new Dictionary<string, ProductRelease>();
            var newPublished = new Dictionary<string, PublishedRelease>();

            var collectionListAsync = database.ListRootCollectionsAsync().ToListAsync().Result;
            foreach (var collection in collectionListAsync)
            {
                if (
                collection.Path.EndsWith(FirebaseHelper.FirebaseCollection[typeof(ReleaseInfo)])
                || collection.Path.EndsWith(FirebaseHelper.FirebaseCollection[typeof(ProductRelease)])
                || collection.Path.EndsWith(FirebaseHelper.FirebaseCollection[typeof(PublishedRelease)]))
                {
                    var documents = collection.GetSnapshotAsync().Result;
                    foreach (var doc in documents)
                    {
                        if (LoadedFirebaseAssets.Contains(doc.Reference.Path))
                            continue;

                        switch(doc.Reference.Path.Split("/documents/")[1].Split("/")[0])
                        {
                            case "Release":
                                var rel = new ReleaseInfo();
                                rel.FromFirebase(doc);
                                newReleaseInfoContent.Add(rel);
                                LoadedFirebaseAssets.Add(doc.Reference.Path);
                                count++;
                                break;
                            case "ProductRelease":
                                var prodrel = new ProductRelease();
                                prodrel.FromFirebase(doc);
                                newReleases.Add(prodrel.ProductID, prodrel);
                                LoadedFirebaseAssets.Add(doc.Reference.Path);
                                count++;
                                count += prodrel.Streams.Length * 2;
                                break;
                            case "PublishedRelease":
                                var pubrel = new PublishedRelease();
                                pubrel.FromFirebase(doc);
                                newPublished.Add(pubrel.CommitHash, pubrel);
                                LoadedFirebaseAssets.Add(doc.Reference.Path);
                                count++;
                                count += pubrel.Files.Length;
                                break;
                        }
                    }
                }
            }

            Releases = new(Releases.Concat(newReleases));
            Published = new(Published.Concat(newPublished));
            ReleaseInfoContent = new(ReleaseInfoContent.Concat(newReleaseInfoContent));
            Console.WriteLine($"[ContentManager->LoadFirebase] Fetched {count} items in {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTimestamp}ms");
        }


        private void databaseDeserialize()
        {
            DatabaseHelper.Read(DATABASE_FILENAME, sr =>
            {
                DatabaseVersion = sr.ReadInt32();
                ReleaseInfoContent = (List<ReleaseInfo>)sr.ReadBList<ReleaseInfo>();
                Releases = (Dictionary<string, ProductRelease>)sr.ReadDictionary<string, ProductRelease>();
                Published = (Dictionary<string, PublishedRelease>)sr.ReadDictionary<string, PublishedRelease>();
                Console.WriteLine($"[ContentManager->databaseDeserialize] Read {Path.GetRelativePath(Directory.GetCurrentDirectory(), DATABASE_FILENAME)}");
            }, (e) =>
            {
                Console.WriteLine(@"//-- content.db is corrupt...".PadLeft(Console.BufferWidth));
                Console.Error.WriteLine(e);
                Console.WriteLine(@"//-- content.db is corrupt...".PadLeft(Console.BufferWidth));
                File.Copy("content.db", $"content.{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.bak.db");
            });
        }
        public void DatabaseSerialize()
        {
            bool response = DatabaseHelper.Write(DATABASE_FILENAME, sw =>
            {
                sw.Write(DatabaseVersion);
                sw.Write(ReleaseInfoContent);
                sw.Write(Releases);
                sw.Write(Published);
            });
            if (response)
            {
                Console.WriteLine($"[ContentManager->DatabaseSerialize] Saved {Path.GetRelativePath(Directory.GetCurrentDirectory(), DATABASE_FILENAME)}");
            }
            else
            {
                Console.Error.WriteLine($"[ContentManager->DatabaseSerialize] Failed to save {Path.GetRelativePath(Directory.GetCurrentDirectory(), DATABASE_FILENAME)}");
            }
        }
    }
}
