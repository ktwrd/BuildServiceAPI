using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security;

namespace BuildServiceAPI.Controllers.Admin.User
{
    [Route("admin/user/permission")]
    [ApiController]
    public class UserPermissionController : Controller
    {
        public static AccountPermission[] RequiredPermissions = new AccountPermission[]
        {
            AccountPermission.USER_PERMISSION_MODIFY
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

            return Json(new ObjectResponse<AccountPermission[]>()
            {
                Success = true,
                Data = account.Permissions.ToArray()
            }, MainClass.serializerOptions);
        }

        [HttpGet("grant")]
        public ActionResult Grant(string token, string username, AccountPermission permission)
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
                Data = account.GrantPermission(permission)
            }, MainClass.serializerOptions);
        }

        [HttpGet("revoke")]
        public ActionResult Revoke(string token, string username, AccountPermission permission)
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
                Data = account.RevokePermission(permission)
            }, MainClass.serializerOptions);
        }
    }
}
