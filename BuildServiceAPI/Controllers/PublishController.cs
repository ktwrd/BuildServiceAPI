using Microsoft.AspNetCore.Mvc;
using BuildServiceCommon.AutoUpdater;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        [HttpPost]
        public ActionResult Index(string token)
        {
            if (!MainClass.ValidTokens.Contains(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            PublishParameters? decodedBody = null;
            try
            {
                decodedBody = Request.ReadFromJsonAsync<PublishParameters>().Result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, "Invalid Body", e), MainClass.serializerOptions);
            }
            if (decodedBody == null)
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, "Invalid Body"), MainClass.serializerOptions);
            }
            var parameters = decodedBody;

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
            var result = new Dictionary<string, bool>()
            {
                { "alreadyPublished", true },
                { "releaseAlreadyExists", true },
                { "attemptScheduleSave", false },
                { "canScheduleSave", MainClass.contentManager?.WillScheduleSave ?? false }
            };
            bool saveRelease = !MainClass.contentManager?.Published.ContainsKey(parameters.releaseInfo.commitHash) ?? false;
            bool saveReleaseInfo = !MainClass.contentManager?.ReleaseInfoContent.Contains(parameters.releaseInfo) ?? false;
            if (saveRelease)
            {
                MainClass.contentManager?.Published.Add(parameters.releaseInfo.commitHash, publishedRelease);
                result["alreadyPublished"] = false;
            }
            if (saveReleaseInfo)
            {
                MainClass.contentManager?.ReleaseInfoContent.Add(parameters.releaseInfo);
                result["releaseAlreadyExists"] = false;
            }
            if (saveRelease || saveReleaseInfo)
            {
                MainClass.contentManager?.ScheduleSave();
                result["attemptScheduleSave"] = true;
            }
            return Json(result, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult All(string token)
        {
            if (!MainClass.ValidTokens.Contains(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            return Json(MainClass.contentManager?.Published ?? new Dictionary<string, PublishedRelease>(), MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("uid/{uid}")]
        public ActionResult All(string uid, string token)
        {
            if (!MainClass.ValidTokens.Contains(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            if (MainClass.contentManager?.Published.ContainsKey(uid) ?? false)
            {
                return Json(MainClass.contentManager.Published[uid], MainClass.serializerOptions);
            }
            else
            {
                return Json(new object(), MainClass.serializerOptions);
            }
        }
    }
}
