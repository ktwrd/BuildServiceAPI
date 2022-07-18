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
            });
        }
        public void DatabaseSerialize()
        {
            DatabaseHelper.Write(DATABASE_FILENAME, sw =>
            {
                sw.Write(DatabaseVersion);
                sw.Write<ReleaseInfo>(ReleaseInfoContent);
                sw.Write(Releases);
                sw.Write(Published);
            });
        }
    }
}
