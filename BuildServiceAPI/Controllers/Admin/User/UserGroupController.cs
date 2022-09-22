using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace BuildServiceAPI.Controllers.Admin.User
{
    [Route("admin/user/group")]
    [ApiController]
    public class UserGroupController : Controller
    {
        public static AccountPermission[] RequiredPermissions = new AccountPermission[]
        {
            AccountPermission.USER_GROUP_MODIFY
        };

        [HttpGet("list")]
        public ActionResult List(string token, string username)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.InvalidCredential
                }, MainClass.serializerOptions);
            }

            var targetAccount = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (targetAccount == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.AccountNotFound(username)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<string[]>()
            {
                Success = true,
                Data = targetAccount.Groups.ToArray()
            }, MainClass.serializerOptions);
        }

        [HttpPost("set")]
        public ActionResult SetContent(string token, string username)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.InvalidCredential
                }, MainClass.serializerOptions);
            }


            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            ObjectResponse<Dictionary<string, string[]>>? decodedBody = null;
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string decodedBodyText = reader.ReadToEnd();
                decodedBody = JsonSerializer.Deserialize<ObjectResponse<Dictionary<string, string[]>>>(decodedBodyText, MainClass.serializerOptions);
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
            if (decodedBody == null)
            {
                Response.StatusCode = 401;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, "Invalid Body")
                }, MainClass.serializerOptions);
            }

            try
            {            
                MainClass.contentManager.AccountManager.SetUserGroups(decodedBody.Data);
            }
            catch (Exception except)
            {
                Response.StatusCode = 500;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(500, $"Exception when invoking MainClass.contentManager.AccountManager.SetUserGroups", except)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<object>()
            {
                Success = true,
                Data = null
            }, MainClass.serializerOptions);
        }

        [HttpGet("grant")]
        public ActionResult Grant(string token, string username, string group)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.InvalidCredential
                }, MainClass.serializerOptions);
            }

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.AccountNotFound(username)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<bool>()
            {
                Success = true,
                Data = account.AddGroup(group)
            }, MainClass.serializerOptions);
        }

        [HttpGet("revoke")]
        public ActionResult RevokeGroup(string token, string username, string group)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.InvalidCredential
                }, MainClass.serializerOptions);
            }

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.AccountNotFound(username)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<bool>()
            {
                Success = true,
                Data = account.RevokeGroup(group)
            }, MainClass.serializerOptions);
        }
    }
}
