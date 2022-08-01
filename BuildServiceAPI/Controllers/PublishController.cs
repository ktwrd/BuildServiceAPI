using Microsoft.AspNetCore.Mvc;
using BuildServiceCommon.AutoUpdater;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using BuildServiceCommon;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        /// <summary>
        /// Body is required to be type of <see cref="PublishParameters"/>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string token)
        {
            if (!MainClass.ValidTokens.ContainsKey(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            if (!Request.HasJsonContentType())
            {
                Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                return Json(new HttpException(401, "Unsupported Media Type"), MainClass.serializerOptions);
            }
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            PublishParameters? decodedBody = null;
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string decodedBodyText = reader.ReadToEnd();
                decodedBody = JsonSerializer.Deserialize<PublishParameters>(decodedBodyText, MainClass.serializerOptions);
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
            if (parameters.releaseInfo.remoteLocation.Length < 1)
                parameters.releaseInfo.remoteLocation = $"{parameters.organization}/{parameters.product}-{parameters.branch}";

            for (int i = 0; i < parameters.files.Count; i++)
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
                fileList.Add(file.ToPublishedReleaseFile(publishedRelease.CommitHash));
            }
            publishedRelease.Files = fileList.ToArray();
            var result = new Dictionary<string, bool>()
            {
                { "alreadyPublished", true },
                { "releaseAlreadyExists", true },
                { "attemptSave", false }
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
                if (MainClass.contentManager != null)
                {
                    MainClass.contentManager.Releases = MainClass.TransformReleaseList(MainClass.contentManager.ReleaseInfoContent.ToArray());
                }
                result["releaseAlreadyExists"] = false;
            }
            if (saveRelease || saveReleaseInfo)
            {
                MainClass.contentManager?.DatabaseSerialize();
                result["attemptSave"] = true;
            }
            return Json(result, MainClass.serializerOptions);
        }

        [HttpGet("all")]
        public ActionResult All(string token)
        {
            if (!MainClass.ValidTokens.ContainsKey(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            return Json(MainClass.contentManager?.Published ?? new Dictionary<string, PublishedRelease>(), MainClass.serializerOptions);
        }

        [HttpGet("hash")]
        public ActionResult ByCommitHashFromParameter(string hash, string token = "")
        {
            if (!MainClass.ValidTokens.ContainsKey(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            if (MainClass.contentManager?.Published.ContainsKey(hash) ?? false)
            {
                return Json(MainClass.contentManager.Published[hash], MainClass.serializerOptions);
            }
            else
            {
                return Json(new object(), MainClass.serializerOptions);
            }
        }

        [HttpGet("hash/{hash}")]
        public ActionResult ByCommitHashFromPath(string token, string hash) => ByCommitHashFromParameter(token, hash);
    }
}
