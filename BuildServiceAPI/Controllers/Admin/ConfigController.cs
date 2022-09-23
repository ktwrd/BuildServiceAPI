using BuildServiceAPI.BuildServiceAPI;
using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace BuildServiceAPI.Controllers.Admin
{
    [Route("admin/[controller]")]
    [ApiController]
    public class ConfigController : Controller
    {
        public static AccountPermission[] RequiredPermissions = new AccountPermission[]
        {
            AccountPermission.ADMINISTRATOR
        };

        [HttpGet]
        [Route("get")]
        public ActionResult GetConfig(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpPost]
        [Route("setvalue")]
        public ActionResult SetValue(string token, string group, string key)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }


            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            object? decodedBody;
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string decodedBodyText = reader.ReadToEnd();
                decodedBody = JsonSerializer.Deserialize<object>(decodedBodyText, MainClass.serializerOptions);
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, "Invalid Body", e)
                }, MainClass.serializerOptions);
            }

            ServerConfig.Set(group, key, decodedBody ?? "");

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }


            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("save")]
        public ActionResult Save(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            ServerConfig.Save();

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }
    }
}
