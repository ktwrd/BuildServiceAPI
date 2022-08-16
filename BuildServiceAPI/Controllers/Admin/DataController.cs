using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using BuildServiceCommon.AutoUpdater;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace BuildServiceAPI.Controllers.Admin
{

    [Route("admin/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        [HttpGet("dump")]
        public ActionResult Dump(string token, DataType type)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.ADMINISTRATOR))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Permissions"
                }, MainClass.serializerOptions);
            }

            if (type == DataType.All)
            {
                return Json(new ObjectResponse<AllDataResult>()
                {
                    Success = true,
                    Data = new AllDataResult()
                    {
                        ReleaseInfoContent = MainClass.contentManager.ReleaseInfoContent,
                        Releases = MainClass.contentManager.Releases,
                        Published = MainClass.contentManager.Published
                    }
                }, MainClass.serializerOptions);
            }
            else if (type == DataType.ReleaseInfoArray)
            {
                return Json(new ObjectResponse<ReleaseInfo[]>()
                {
                    Success = true,
                    Data = MainClass.contentManager.ReleaseInfoContent.ToArray()
                }, MainClass.serializerOptions);
            }
            else if (type == DataType.ReleaseDict)
            {
                return Json(new ObjectResponse<Dictionary<string, ProductRelease>>()
                {
                    Success = true,
                    Data = MainClass.contentManager.Releases
                }, MainClass.serializerOptions);
            }
            else if (type == DataType.PublishDict)
            {
                return Json(new ObjectResponse<Dictionary<string, PublishedRelease>>()
                {
                    Success = true,
                    Data = MainClass.contentManager.Published
                }, MainClass.serializerOptions);
            }
            else
            {
                return Json(new ObjectResponse<string>()
                {
                    Success = true,
                    Data = "Invalid parameter \"type\""
                }, MainClass.serializerOptions);
            }
        }
        [HttpPost("setdata")]
        public ActionResult SetData(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.ADMINISTRATOR))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Permissions"
                }, MainClass.serializerOptions);
            }
            HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            string content = new StreamReader(HttpContext.Request.Body).ReadToEndAsync().Result.ReplaceLineEndings("");
            var deserializedDynamic = JsonSerializer.Deserialize<ObjectResponse<dynamic>>(WebUtility.UrlDecode(content), MainClass.serializerOptions);
            Type targetType = Type.GetType(deserializedDynamic.DataType);
            bool success = false;
            if (targetType == typeof(ReleaseInfo[]))
            {
                MainClass.contentManager.ReleaseInfoContent = new List<ReleaseInfo>(deserializedDynamic.Data);
                success = true;
            }
            else if (targetType == MainClass.contentManager.Releases.GetType())
            {
                var des = JsonSerializer.Deserialize<ObjectResponse<Dictionary<string, ProductRelease>>>(content, MainClass.serializerOptions);
                MainClass.contentManager.Releases = des.Data;
                success = true;
            }
            else if (targetType == MainClass.contentManager.Published.GetType())
            {
                var des = JsonSerializer.Deserialize<ObjectResponse<Dictionary<string, PublishedRelease>>>(content, MainClass.serializerOptions);
                MainClass.contentManager.Published = des.Data;
                success = true;
            }
            else if (targetType == typeof(AllDataResult))
            {
                var des = JsonSerializer.Deserialize<ObjectResponse<AllDataResult>>(content, MainClass.serializerOptions);

                var c = des.Data;
                MainClass.contentManager.ReleaseInfoContent = new List<ReleaseInfo>(c.ReleaseInfoContent);
                MainClass.contentManager.Releases = c.Releases;
                MainClass.contentManager.Published = c.Published;
                success = true;
            }

            if (success)
            {
                MainClass.contentManager.DatabaseSerialize();
                return Json(new ObjectResponse<object>()
                {
                    Success = true,
                    Data = deserializedDynamic.Data,
                    DataType = deserializedDynamic.DataType
                }, MainClass.serializerOptions);
            }
            else
            {
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid parameter \"type\""
                }, MainClass.serializerOptions);
            }

        }
    }
}
