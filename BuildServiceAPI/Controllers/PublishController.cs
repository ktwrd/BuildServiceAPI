using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;
using System.Text.Json;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        [HttpGet]
        public ActionResult Index(string token, string organization, string product, string branch, long timestamp, string encodedReleaseInfo, string encodedSendDataArray)
        {
            if (!MainClass.ValidTokens.Contains(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            var jsonDeserializeOptions = new JsonSerializerOptions()
            {
                IncludeFields = true,
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false,
                WriteIndented = true
            };

            var parameters = new PublishParameters()
            {
                token = token,
                organization = organization,
                product = product,
                branch = branch,
                timestamp = timestamp,
                releaseInfo = JsonSerializer.Deserialize<ReleaseInfo>(encodedReleaseInfo, jsonDeserializeOptions),
                files = JsonSerializer.Deserialize<ManagedUploadSendData[]>(encodedSendDataArray, jsonDeserializeOptions) ?? Array.Empty<ManagedUploadSendData>()
            };
            for (int i = 0; i < parameters.files.Length; i++)
            {
                parameters.files[i].ETag = parameters.files[i].ETag.Replace("\"", @"");
            }

            var fileList = new List<PublishedReleaseFile>();
            var publishedRelease = new PublishedRelease()
            {
                CommitHash = parameters.releaseInfo.commitHash,
                Timestamp = parameters.releaseInfo.envtimestamp,
                Release = parameters.releaseInfo
            };
            foreach (var file in parameters.files)
            {
                var prf = new PublishedReleaseFile()
                {
                    Location = file.Location,
                    CommitHash = publishedRelease.CommitHash
                };
                if (file.Location.EndsWith(@"win-amd64.exe"))
                {
                    prf.Platform = FilePlatform.Windows;
                    prf.Type = FileType.Installer;
                }
                else if (file.Location.EndsWith(@"win-amd64.zip"))
                {
                    prf.Platform = FilePlatform.Windows;
                    prf.Type = FileType.Portable;
                }
                else if (file.Location.EndsWith(@".tar.gz") || file.Location.EndsWith(@"linux-amd64.tar.gz"))
                {
                    prf.Platform = FilePlatform.Linux;
                    prf.Type = FileType.Portable;
                }

                fileList.Add(prf);
            }
            publishedRelease.Files = fileList.ToArray();
            if (!MainClass.contentManager.Published.ContainsKey(parameters.releaseInfo.commitHash))
            {
                MainClass.contentManager.Published.Add(parameters.releaseInfo.commitHash, publishedRelease);
                MainClass.Save();
            }
            return Json(parameters, jsonDeserializeOptions);
        }
    }
}
