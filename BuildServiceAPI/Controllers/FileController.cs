using BuildServiceCommon;
using BuildServiceCommon.AutoUpdater;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        [HttpPost]
        [Route("{hash}")]
        public ActionResult AddFileToHash(string hash, string token)
        {
            if (token == null || token.Length < 1 || !MainClass.ValidTokens.ContainsKey(token) || !MainClass.contentManager.AccountManager.AccountHasPermission(token, BuildServiceCommon.Authorization.AccountPermission.CREATE_RELEASE))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            else if (!MainClass.contentManager?.Published.ContainsKey(hash) ?? false)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new HttpException(404, @"Commit not published"), MainClass.serializerOptions);
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

            List<ManagedUploadSendData>? decodedBody = null;
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string decodedBodyText = reader.ReadToEnd();
                decodedBody = JsonSerializer.Deserialize<List<ManagedUploadSendData>>(decodedBodyText, MainClass.serializerOptions);
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

            var fileList = new List<PublishedReleaseFile>();
            foreach (var i in decodedBody)
                fileList.Add(i.ToPublishedReleaseFile(hash));
            if (MainClass.contentManager == null)
            {
                Response.StatusCode = 500;
                return Json(new HttpException(500, "Content Manager has not been initalized"), MainClass.serializerOptions);
            }
            MainClass.contentManager.Published[hash].Files =
            new List<PublishedReleaseFile>(MainClass.contentManager?.Published[hash].Files ?? new PublishedReleaseFile[] { })
                .Concat(fileList)
                .ToArray();
            return Json(MainClass.contentManager?.Published[hash].Files ?? Array.Empty<object>(), MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("{hash}")]
        public ActionResult FetchFilesFromHash(string hash, string token = "")
        {
            var returnContent = new List<PublishedReleaseFile>();
            var contentManager = MainClass.contentManager;
            if (contentManager != null)
            {
                if (contentManager.Published.ContainsKey(hash))
                {
                    var allow = false;
                    var commit = contentManager.Published[hash];
                    if (commit.Release.releaseType == ReleaseType.Other)
                    {
                        allow = MainClass.contentManager.AccountManager.AccountHasPermission(token, BuildServiceCommon.Authorization.AccountPermission.READ_RELEASE_BYPASS);
                    }
                    else
                    {
                        allow = true;
                    }
#if BUILDSERVICEAPI_APP_WHITELIST
                    if (commit.Release.appID == "com.minalyze.minalogger")
                    {
                        if (MainClass.UserByTokenHasService(token, "ml2") && commit.Release.releaseType != ReleaseType.Other && commit.Release.releaseType != ReleaseType.Invalid)
                            allow = true;
                        else if (MainClass.UserByTokenIsAdmin(token))
                        {
                            allow = true;
                        }
                    }
                    else
                    {
                        allow = true;
                    }
#endif
                    if (allow)
                        returnContent = new List<PublishedReleaseFile>(commit.Files);
                }
            }
            return Json(returnContent, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("")]
        public ActionResult FetchFilesFromHashByParameter(string hash, string token = "") => FetchFilesFromHash(hash, token);
    }
}
