using BuildServiceAPI.BuildServiceAPI;
using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using static Google.Rpc.Context.AttributeContext.Types;

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

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, string>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("setvalue")]
        public ActionResult SetValue(string token, string group, string key, string value)
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

            ServerConfig.Set(group, key, value);

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, string>>>()
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

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, string>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }
    }
}
