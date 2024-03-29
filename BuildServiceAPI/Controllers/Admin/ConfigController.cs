﻿using BuildServiceAPI.BuildServiceAPI;
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

        [HttpGet("get")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, Dictionary<string, object>>>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult GetConfig(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, ServerStringResponse.InvalidCredential)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpPost("setvalue")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, Dictionary<string, object>>>))]
        [ProducesResponseType(400, Type = typeof(ObjectResponse<HttpException>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult SetValue(string token, string group, string key)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, ServerStringResponse.InvalidCredential)
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
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(StatusCodes.Status400BadRequest, ServerStringResponse.InvalidBody, e)
                }, MainClass.serializerOptions);
            }

            ServerConfig.Set(group, key, decodedBody ?? "");

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpPost("set")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, Dictionary<string, object>>>))]
        [ProducesResponseType(400, Type = typeof(ObjectResponse<HttpException>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult SetConfig(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, ServerStringResponse.InvalidCredential)
                }, MainClass.serializerOptions);
            }


            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            Dictionary<string, Dictionary<string, object>>? decodedBody;
            try
            {
                StreamReader reader = new StreamReader(Request.Body);
                string decodedBodyText = reader.ReadToEnd();
                decodedBody = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(decodedBodyText, MainClass.serializerOptions);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(400, ServerStringResponse.InvalidBody, e)
                }, MainClass.serializerOptions);
            }
            if (decodedBody == null)
            {
                Response.StatusCode = 400;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(400, ServerStringResponse.InvalidBody)
                }, MainClass.serializerOptions);
            }

            ServerConfig.Set(decodedBody);

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpPost("reset")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, Dictionary<string, object>>>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        [ProducesResponseType(500, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult ResetConfig(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, ServerStringResponse.InvalidCredential)
                }, MainClass.serializerOptions);
            }

            try
            {
                ServerConfig.Set(ServerConfig.DefaultData);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(500, $"Failed to set config", e)
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<Dictionary<string, Dictionary<string, object>>>()
            {
                Data = ServerConfig.Get(),
                Success = true
            }, MainClass.serializerOptions);
        }

        [HttpGet("save")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, Dictionary<string, object>>>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult Save(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(401, ServerStringResponse.InvalidCredential)
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
