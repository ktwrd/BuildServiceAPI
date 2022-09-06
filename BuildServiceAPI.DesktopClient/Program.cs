using Nini.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildServiceAPI.DesktopClient
{
    public static class Program
    {
        public static string ConfigFilename => "client.ini";
        public static string ConfigLocation => Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ConfigFilename);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            serializerOptions.Converters.Add(new kate.shared.DateTimeConverterUsingDateTimeOffsetParse());
            serializerOptions.Converters.Add(new kate.shared.DateTimeConverterUsingDateTimeParse());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static void Save()
        {
            Properties.Settings.Default.Save();
            UserConfig.Save();
        }
        public static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = true,
            WriteIndented = true,
        };

        internal static void InitializeConfig()
        {
            UserConfig.Set("Authentication", "Username", "");
            UserConfig.Set("Authentication", "Password", "");
            UserConfig.Set("Authentication", "Endpoint", "");
            UserConfig.Set("General", "ShowLatestRelease", true);
        }
    }
}
