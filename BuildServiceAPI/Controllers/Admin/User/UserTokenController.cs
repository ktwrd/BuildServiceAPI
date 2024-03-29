﻿using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using static BuildServiceCommon.Authorization.AccountManager;
using Account = BuildServiceCommon.Authorization.Account;


namespace BuildServiceAPI.Controllers.Admin.User
{
    [Route("admin/user/token")]
    [ApiController]
    public class UserTokenController : Controller
    {
        public AccountPermission[] RequiredPermissions = new AccountPermission[]
        {
            AccountPermission.USER_TOKEN_PURGE
        };

        [HttpGet("purge")]
        [ProducesResponseType(200, Type = typeof(ObjectResponse<Dictionary<string, int>>))]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult TokenPurge(string token, string? username = null, bool? isUsernameFieldRegexPattern = false)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Json(new ObjectResponse<HttpException>()
                {
                    Success = false,
                    Data = new HttpException(StatusCodes.Status401Unauthorized, ServerStringResponse.InvalidCredential)
                }, MainClass.serializerOptions);
            }

            BuildServiceCommon.Authorization.Account[] accountArray;

            if ((isUsernameFieldRegexPattern ?? false) && username != null && username.Length > 0)
            {
                if (username == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new ObjectResponse<string>()
                    {
                        Success = false,
                    }, MainClass.serializerOptions);
                }
                Regex expression;
                try
                {
                    expression = new Regex(username);
                }
                catch (Exception except)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new ObjectResponse<HttpException>()
                    {
                        Success = false,
                        Data = new HttpException((int)HttpStatusCode.InternalServerError, @"Failed to create expression", except)
                    }, MainClass.serializerOptions);
                }

                accountArray = MainClass.contentManager.AccountManager.GetAccountsByRegex(expression, AccountField.Username).ToArray();
            }
            // When username is null, that means we want to purge our own tokens.
            else if (username == null)
            {
                accountArray = new BuildServiceCommon.Authorization.Account[]
                {
                    MainClass.contentManager.AccountManager.GetAccount(token)
                };
            }
            else
            {
                accountArray = new BuildServiceCommon.Authorization.Account[]
                {
                    MainClass.contentManager.AccountManager.GetAccountByUsername(username)
                };
            }

            var usernameDict = new Dictionary<string, int>();
            foreach (var a in accountArray)
            {
                usernameDict.Add(a.Username, a.RemoveTokens());
            }

            return Json(new ObjectResponse<Dictionary<string, int>>()
            {
                Success = true,
                Data = usernameDict
            }, MainClass.serializerOptions);
        }
    }
}
