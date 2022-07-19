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

        private Thread firebaseSaveThread;
        private Thread firebaseLoadThread;
        public ContentManager()
        {
            BusStationTimer = new System.Threading.Timer(CheckBusSchedule, new AutoResetEvent(false), 0, 150);
            TriggerSave = false;
            TriggerSaveTimestamp = -1;
            TriggerLoad = false;
            TriggerLoadTimestamp = -1;
            IsSaving = false;
            IsLoading = false;
            firebaseSaveThread = new Thread(firebaseSaveThreadLogic);
            firebaseLoadThread = new Thread(firebaseLoadThreadLogic);
            database = FirestoreDb.Create(@"cloudtesting-3d734");

            // We do this because we want to lock the process
            // before the web server starts, just to be safe.
            firebaseLoadThreadLogic();
        }
        static ContentManager()
        {
            FirebaseHelper.FirebaseCollection.Add(typeof(PublishedRelease), "PublishedRelease");
            FirebaseHelper.FirebaseCollection.Add(typeof(PublishedReleaseFile), "PublishedReleaseFile");
        }

        private Timer BusStationTimer;

        #region Schedule Saving
        public bool TriggerSave { get; private set; }
        public long TriggerSaveTimestamp { get; private set; }
        public void ScheduleSave()
        {
            if (TriggerSave || TriggerLoad || TriggerSaveTimestamp > 0) return;
            TriggerSave = true;
            TriggerSaveTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        #endregion
        #region Schedule Loading
        public bool TriggerLoad { get; set; }
        public long TriggerLoadTimestamp { get; private set; }
        public void ScheduleLoad()
        {
            if (TriggerLoad || TriggerSave || TriggerLoadTimestamp > 0) return;
            Console.WriteLine($"ScheduleLoad: (TriggerLoad: {TriggerLoad}, TriggerSave: {TriggerSave}, TriggerLoadTimestamp: {TriggerLoadTimestamp})");
            TriggerLoad = true;
            TriggerLoadTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
        #endregion
        #region Schedule Checker
        public bool IsSaving{ get; private set; }
        public bool IsLoading { get; private set; }
        private void CheckBusSchedule(object? state)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            if (TriggerLoadTimestamp > 0 && !IsSaving && !IsLoading)
            {
                if ((TriggerLoadTimestamp < TriggerSaveTimestamp || TriggerSaveTimestamp <= 0) && TriggerLoad)
                {
                    TriggerLoad = false;
                    LoadFirebase();
                }
            }
            if (TriggerSaveTimestamp > 0 && !IsSaving && !IsLoading)
            {
                if ((TriggerSaveTimestamp < TriggerLoadTimestamp || TriggerLoadTimestamp <= 0) && TriggerSave)
                {
                    TriggerSave = false;
                    SaveFirebase();
                }
            }
            autoEvent.Set();
        }
        #endregion

        #region Save Content from Firebase
        public void SaveFirebase()
        {
            if (firebaseSaveThread.ThreadState == ThreadState.Running) return;
            if (firebaseSaveThread.ThreadState == ThreadState.Stopped)
                firebaseSaveThread = new Thread(firebaseSaveThreadLogic);
            firebaseSaveThread.Start();
        }
        private void firebaseSaveThreadLogic()
        {
            Console.WriteLine($"[ContentManager->SaveFirebase] Uploading Content to Firebase");
            IsSaving = true;
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
            TriggerSaveTimestamp = -1;
            IsSaving = false;
        }
        #endregion
        #region Load Content from Firebase
        public void LoadFirebase()
        {
            if (firebaseLoadThread.ThreadState == ThreadState.Running) return;
            if (firebaseLoadThread.ThreadState == ThreadState.Stopped)
                firebaseLoadThread = new Thread(firebaseLoadThreadLogic);
            firebaseLoadThread.Start();
        }
        private void firebaseLoadThreadLogic()
        {
            IsLoading = true;
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

                        switch (doc.Reference.Path.Split("/documents/")[1].Split("/")[0])
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
            TriggerLoadTimestamp = -1;
            IsLoading = false;
        }
        #endregion
    }
}
