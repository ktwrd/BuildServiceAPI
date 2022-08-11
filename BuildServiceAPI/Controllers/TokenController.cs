using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BuildServiceAPI.Controllers
{
    public class ObjectResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string DataType => Data?.GetType().ToString();
    }

    [Route("[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpGet]
        [Route("grant")]
        public ActionResult Grant(string username, string password)
        {
            var grantTokenResponse = MainClass.contentManager.AccountManager.GrantTokenAndOrAccount(username, password);

            if (!grantTokenResponse.Success)
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new ObjectResponse<GrantTokenResponse>()
            {
                Success = grantTokenResponse.Success,
                Data = grantTokenResponse
            });
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
                });
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
                });
            }
            catch (Exception)
            { }

            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new ObjectResponse<bool>()
            {
                Success = false,
                Data = false
            });
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
                });
            }

            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                });
            }

            var details = account.GetTokenDetails(token);
            if (details == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                });
            }

            return Json(new ObjectResponse<AccountTokenDetailsResponse>()
            {
                Success = true,
                Data = details
            });
        }

        [HttpGet]
        [Route("remove")]
        public ActionResult Reset(string token, bool all = false)
        {
            if (token == null || token.Length < 32 || token.Length > 32)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                });
            }

            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = "Invalid Account"
                });
            }

            if (all)
            {
                account.RemoveTokens();
                return Json(new ObjectResponse<object>()
                {
                    Success = true,
                    Data = null
                });
            }
            else
            {
                account.RemoveToken(token);
                return Json(new ObjectResponse<object>()
                {
                    Success = true,
                    Data = null
                });
            }
        }
    }
}
