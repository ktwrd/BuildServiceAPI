using Minalyze.Shared.AutoUpdater;
using Minalyze.Shared.Helpers;

namespace BuildServiceAPI
{
    public class ContentManager
    {
        public List<ReleaseInfo> ReleaseInfoContent = new();
        public Dictionary<string, ProductRelease> Releases = new();
        public Dictionary<string, PublishedRelease> Published = new();

        internal int DatabaseVersion;

        private string DATABASE_FILENAME = Path.Join(
            Directory.GetCurrentDirectory(),
            @"content.db");

        public ContentManager()
        {
            databaseDeserialize();
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
            }, () =>
            {
                Console.WriteLine(@"//-- content.db is corrupt...".PadLeft(Console.BufferWidth));
                File.Copy("content.db", $"content.db.{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
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
