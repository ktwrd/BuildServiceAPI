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
    public class UserController : Controller
    {
        public static AccountPermission[] RequiredPermissions = new AccountPermission[]
        {
            AccountPermission.ADMINISTRATOR
        };
        public static bool IsAllowed(string token) => MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions);
        public static bool IsAllowed(Account account) => MainClass.contentManager.AccountManager.AccountHasPermission(account, RequiredPermissions);

        [HttpGet]
        [Route("list")]
        public ActionResult List(string token, string username=null, SearchMethod usernameSearchType = SearchMethod.Equals, long firstSeenTimestamp=0, long lastSeenTimestamp=long.MaxValue)
        {
            if (MainClass.contentManager.AccountManager.AccountHasPermission(token, RequiredPermissions))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            var detailList = new List<AccountDetailsResponse>();
            foreach (var account in MainClass.contentManager.AccountManager.AccountList)
            {
                if (account.FirstSeenTimestamp >= firstSeenTimestamp
                    && account.LastSeenTimestamp <= lastSeenTimestamp)
                {
                    if (username == null)
                        detailList.Add(account.GetDetails());
                    else
                    {
                        bool cont = false;
                        switch (usernameSearchType)
                        {
                            case SearchMethod.Equals:
                                cont = username == account.Username;
                                break;
                            case SearchMethod.StartsWith:
                                cont = account.Username.StartsWith(username);
                                break;
                            case SearchMethod.EndsWith:
                                cont = account.Username.EndsWith(username);
                                break;
                            case SearchMethod.Includes:
                                cont = account.Username.Contains(username);
                                break;
                            case SearchMethod.IncludesNot:
                                cont = !account.Username.Contains(username);
                                break;
                        }
                        if (cont)
                            detailList.Add(account.GetDetails());
                    }
                }
            }
            return Json(new ObjectResponse<AccountDetailsResponse[]>()
            {
                Success = true,
                Data = detailList.ToArray()
            }, MainClass.serializerOptions);
        }
    
        [HttpGet("permission/grant")]
        public ActionResult GrantPermission(string token, string username, AccountPermission permission)
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

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = $"Could not find account with username of \"{username}\""
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<bool>()
            {
                Success = true,
                Data = account.GrantPermission(permission)
            }, MainClass.serializerOptions);
        }

        [HttpGet("permission/revoke")]
        public ActionResult RevokePermission(string token, string username, AccountPermission permission)
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

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = $"Could not find account with username of \"{username}\""
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<bool>()
            {
                Success = true,
                Data = account.RevokePermission(permission)
            }, MainClass.serializerOptions);
        }
    
        [HttpGet("group/grant")]
        public ActionResult GrantGroup(string token, string username, string group)
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

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = $"Could not find account with username of \"{username}\""
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<bool>()
            {
                Success = true,
                Data = account.AddGroup(group)
            }, MainClass.serializerOptions);
        }

        [HttpGet("group/revoke")]
        public ActionResult RevokeGroup(string token, string username, string group)
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

            var account = MainClass.contentManager.AccountManager.GetAccountByUsername(username);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = $"Could not find account with username of \"{username}\""
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
