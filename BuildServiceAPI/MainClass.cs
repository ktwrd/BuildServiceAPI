using BuildServiceCommon.AutoUpdater;
using kate.shared.Helpers;
using System.Security.Cryptography;
using System.Text.Json;

namespace BuildServiceAPI
{
    public static class MainClass
    {
        public static WebApplicationBuilder? Builder;
        public static WebApplication? App;
        /// <summary>
        /// <para>
        /// Key is the Token
        /// </para>
        /// <para>
        /// Value is the SHA256 of (Username + Password)
        /// </para>
        /// </summary>
        public static Dictionary<string, string> ValidTokens = new Dictionary<string, string>();
        public static List<ITokenGranter> TokenGrantList = new List<ITokenGranter>();

        public static ContentManager? contentManager;

        public static void Main(string[] args)
        {
            contentManager = new ContentManager();
            LoadTokens();
            Builder = WebApplication.CreateBuilder(args);
            Builder.Services.AddControllers();

            if (Builder.Environment.IsDevelopment())
            {
                Builder.Services.AddSwaggerGen();
            }

            App = Builder.Build();

            if (App.Environment.IsDevelopment())
            {
                App.UseSwagger();
                App.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                Console.WriteLine($"[BuildServiceAPI->Main] In development mode, so swagger is enabled. SwaggerUI can be accessed at 0.0.0.0:5010/");
            }

            App.UseAuthorization();
            App.MapControllers();
            App.Run();
        }

        public static void Save()
        {
            contentManager?.DatabaseSerialize();
        }
        public static string RegisterToken(string validatorSignature, string username, string password)
        {
            SHA256 sha256Instance = SHA256.Create();
            var computedHash = sha256Instance.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{username}{password}"));
            var hash = GeneralHelper.Base62Encode(computedHash);
            var token = GeneralHelper.GenerateToken(32);
            ValidTokens.Add(token, hash);
            return token;
        }

        public static void LoadTokens()
        {
            if (!File.Exists(@"tokens.json"))
            {
                File.WriteAllText(@"tokens.json", @"[]");
                return;
            }
            else
            {
                var content = File.ReadAllText(@"tokens.json");
                var response = JsonSerializer.Deserialize<List<string>>(content, serializerOptions); ;
                if (response == null)
                {
                    Console.Error.WriteLine($"Failed to parse 'tokens.json' with content of\n{content}");
                    return;
                }
                var dict = new Dictionary<string, string>();
                foreach (var i in response)
                    dict.Add(i, "");
                ValidTokens = dict;
            }
        }

        public static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = true
        };

        public static string[] GetFileList(string directory, string filename)
        {
            var allFiles = new List<string>();
            foreach (var file in Directory.GetFiles(directory))
            {
                if (file.EndsWith(filename))
                    allFiles.Add(file);
            }
            foreach (var dir in Directory.GetDirectories(directory))
            {
                allFiles = new List<string>(allFiles.Concat(GetFileList(dir, filename)));
            }
            return allFiles.ToArray();
        }

        public static List<ReleaseInfo> ScrapeForProducts(string[] infoFiles)
        {
            var releaseList = new List<ReleaseInfo>();
            foreach (var file in infoFiles)
            {
                var content = File.ReadAllText(file);
                if (content.Replace(": ", ":").Contains("\"envtimestamp\":\"")) continue;
                var deserialized = JsonSerializer.Deserialize<ReleaseInfo>(content, serializerOptions);
                if (deserialized == null || deserialized?.envtimestamp == null) continue;

                var splitted = file.Split(Path.DirectorySeparatorChar);
                var organization = splitted[splitted.Length - 4];
                var repository = splitted[splitted.Length - 3];
                if (deserialized.remoteLocation.Length < 1)
                    deserialized.remoteLocation = $"{organization}/{repository}";

                var recognitionMap = new Dictionary<ReleaseType, string[]>()
                {
                    {ReleaseType.Beta, new string[] {
                        "-beta",
                        "-canary"
                    } },
                    {ReleaseType.Nightly, new string[] {
                        "-dev",
                        "-devel",
                        "-debug",
                        "-nightly"
                    } },
                    {ReleaseType.Stable, new string[] {
                        "-stable",
                        "-public",
                        "-prod",
                        "-main"
                    } }
                };
                var targetReleaseType = ReleaseType.Invalid;

                foreach (var pair in recognitionMap)
                {
                    var pairTarget = ReleaseType.Invalid;
                    foreach (var item in pair.Value)
                    {
                        if (pairTarget != ReleaseType.Invalid && (repository.EndsWith(item) || deserialized.remoteLocation.EndsWith(item)))
                            pairTarget = pair.Key;
                    }
                    if (pairTarget != ReleaseType.Invalid)
                    {
                        targetReleaseType = pairTarget;
                        break;
                    }
                }
                if (repository.Split('-').Length == 1 || deserialized.remoteLocation.Split('-').Length == 1)
                    targetReleaseType = ReleaseType.Stable;
                else if (targetReleaseType == ReleaseType.Invalid)
                    targetReleaseType = ReleaseType.Other;

                deserialized.releaseType = targetReleaseType;
                if ((deserialized.files.ContainsKey("windows") && deserialized.executable.ContainsKey("windows")) ||
                    (deserialized.files.ContainsKey("linux")   && deserialized.executable.ContainsKey("linux")))
                    releaseList.Add(deserialized);
            }
            return releaseList;
        }
        public static Dictionary<string, ProductRelease> TransformReleaseList(ReleaseInfo[] releases)
        {
            var products = new Dictionary<string, List<ProductReleaseStream>>();
            var productIDLink = new Dictionary<string, List<string>>();

            foreach (var release in releases)
            {
                if (release.executable == null || release.appID == null || release.appID.Length < 1) continue;

                var executable = new ProductExecutable()
                {
                    Linux = release.executable["linux"],
                    Windows = release.executable["windows"]
                };
                if (release.releaseType == ReleaseType.Other)
                {
                    var recognitionMap = new Dictionary<ReleaseType, string[]>()
                    {
                        {ReleaseType.Beta, new string[] {
                            "-beta",
                            "-canary"
                        } },
                        {ReleaseType.Nightly, new string[] {
                            "-dev",
                            "-devel",
                            "-debug",
                            "-nightly"
                        } },
                        {ReleaseType.Stable, new string[] {
                            "-stable",
                            "-public",
                            "-prod",
                            "-main"
                        } }
                    };
                    var targetReleaseType = ReleaseType.Invalid;

                    foreach (var pair in recognitionMap)
                    {
                        var pairTarget = ReleaseType.Invalid;
                        foreach (var item in pair.Value)
                        {
                            if (pairTarget == ReleaseType.Invalid && (release.remoteLocation.Split('/')[1].EndsWith(item) || release.remoteLocation.EndsWith(item)))
                                pairTarget = pair.Key;
                        }
                        if (pairTarget != ReleaseType.Invalid)
                        {
                            targetReleaseType = pairTarget;
                            break;
                        }
                    }
                    if (release.remoteLocation.Split('/')[1].Split('-').Length == 1 || release.remoteLocation.Split('-').Length == 1)
                        targetReleaseType = ReleaseType.Stable;
                    else if (targetReleaseType == ReleaseType.Invalid)
                        targetReleaseType = ReleaseType.Other;
                    release.releaseType = targetReleaseType;
                }
                var stream = new ProductReleaseStream()
                {
                    ProductName = release.name,
                    ProductVersion = release.version,
                    ProductExpiryTimestamp = 0,
                    BranchName = release.releaseType.ToString(),
                    UpdatedTimestamp = release.envtimestamp,
                    UpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(release.envtimestamp),
                    RemoteSignature = release.remoteLocation,
                    Executable = executable,
                    ProductID = release.appID,
                    CommitHash = release.commitHash
                };
                if (!products.ContainsKey(release.appID))
                    products.Add(release.appID, new List<ProductReleaseStream>());
                if (!productIDLink.ContainsKey(release.appID))
                    productIDLink.Add(release.appID, new List<string>());
                if (!productIDLink[release.appID].Contains(release.productName))
                    productIDLink[release.appID].Add(release.productName);
                products[release.appID].Add(stream);
            }

            var releaseTable = new Dictionary<string, ProductRelease>();
            foreach (var pair in products)
            {
                var release = new ProductRelease()
                {
                    ProductName = productIDLink[pair.Key].FirstOrDefault(pair.Key),
                    ProductID = pair.Key,
                    Streams = pair.Value.ToArray()
                };
                releaseTable.Add(pair.Key, release);
            }

            return releaseTable;
        }

        public static List<ProductRelease> Products = new List<ProductRelease>();
    }
}
