using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BuildServiceAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpGet]
        [Route("grant")]
        public ActionResult Grant(string username, string password)
        {
            var grantTokenResponse = MainClass.contentManager.AccountManager.GrantTokenAndOrAccount(WebUtility.UrlDecode(username), WebUtility.UrlDecode(password));

            if (!grantTokenResponse.Success)
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new ObjectResponse<GrantTokenResponse>()
            {
                Success = grantTokenResponse.Success,
                Data = grantTokenResponse
            }, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("validate")]
        public ActionResult Validate(string token)
        {
            if (token.Length < 32 || token.Length > 32)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<bool>()
                {
                    Success = false,
                    Data = false
                }, MainClass.serializerOptions);
            }

            try
            {
                var res = MainClass.contentManager.AccountManager.ValidateToken(token);
                if (!res)
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                return Json(new ObjectResponse<bool>()
                {
                    Success = res,
                    Data = res
                }, MainClass.serializerOptions);
            }
            catch (Exception)
            { }

            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new ObjectResponse<bool>()
            {
                Success = false,
                Data = false
            }, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("details")]
        public ActionResult Details(string token)
        {
            if (token == null || token.Length < 32 || token.Length > 32)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            var details = account.GetTokenDetails(token);
            if (details == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            return Json(new ObjectResponse<AccountTokenDetailsResponse>()
            {
                Success = true,
                Data = details
            }, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("remove")]
        public ActionResult Reset(string token, bool? all = false)
        {
            if (token == null || token.Length < 32 || token.Length > 32)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                }, MainClass.serializerOptions);
            }

            if (all ?? false)
            {
                account.RemoveTokens();
                return Json(new ObjectResponse<object>()
                {
                    Success = true,
                    Data = null
                }, MainClass.serializerOptions);
            }
            else
            {
                account.RemoveToken(token);
                return Json(new ObjectResponse<object>()
                {
                    Success = true,
                    Data = null
                }, MainClass.serializerOptions);
            }
        }
    }
}
