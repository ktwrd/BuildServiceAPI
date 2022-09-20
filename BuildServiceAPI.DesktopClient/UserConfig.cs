using Nini.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildServiceAPI.DesktopClient
{
    public static class UserConfig
    {
        public static string ConfigFilename => "client.ini";
        public static string ConfigLocation => Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ConfigFilename);
        public static IniConfigSource Source;
        static UserConfig()
        {
            if (!File.Exists(ConfigLocation))
                File.WriteAllText(ConfigLocation, "");
            Source = new IniConfigSource(ConfigLocation);   
        }

        public static void Save() => Source.Save();

        public static IConfig Get(string group)
        {
            var cfg = Source.Configs[group];
            if (cfg == null)
                cfg = Source.Configs.Add(group);
            return cfg;
        }
        public static void Set(string group, string key, object value)
        {
            var cfg = Get(group);
            cfg.Set(key, value);
        }

        public static string Get(string group, string key) => Get(group).Get(key);
        public static string Get(string group, string key, string fallback) => Get(group).Get(key, fallback);

        public static string GetExpanded(string group, string key) => Get(group).GetExpanded(key);

        public static string GetString(string group, string key) => Get(group).GetString(key);
        public static string GetString(string group, string key, string fallback) => Get(group).GetString(key, fallback);

        public static int GetInt(string group, string key) => Get(group).GetInt(key);
        public static int GetInt(string group, string key, int fallback) => Get(group).GetInt(key, fallback);
        public static int GetInt(string group, string key, int fallback, bool fromAlias) => Get(group).GetInt(key, fallback, fromAlias);

        public static long GetLong(string group, string key) => Get(group).GetLong(key);
        public static long GetLong(string group, string key, long fallback) => Get(group).GetLong(key, fallback);

        public static bool GetBoolean(string group, string key) => Get(group).GetBoolean(key);
        public static bool GetBoolean(string group, string key, bool fallback) => Get(group).GetBoolean(key, fallback);

        public static float GetFloat(string group, string key) => Get(group).GetFloat(key);
        public static float GetFloat(string group, string key, float fallback) => Get(group).GetFloat(key, fallback);

        public static string[] GetKeys(string group) => Get(group).GetKeys();
        public static string[] GetValues(string group) => Get(group).GetValues();
        public static void Remove(string group, string key) => Get(group).Remove(key);
    }
}
