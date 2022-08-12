using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using BuildServiceCommon.AutoUpdater;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

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
        [HttpGet("setdata")]
        public ActionResult SetData(string token, string content)
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
                MainClass.contentManager.Releases = deserializedDynamic.Data;
                success = true;
            }
            else if (targetType == MainClass.contentManager.Published.GetType())
            {
                MainClass.contentManager.Published = deserializedDynamic.Data;
                success = true;
            }
            else if (targetType == typeof(AllDataResult))
            {
                var c = (AllDataResult)deserializedDynamic.Data;
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
                    Data = deserializedDynamic.Data
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
