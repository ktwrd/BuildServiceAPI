using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuildServiceAPI.Controllers.Admin.Bot
{
    [Route("admin/bot")]
    [ApiController]
    public class BotListingController : Controller
    {
        [HttpGet("list")]
        [ProducesResponseType(401, Type = typeof(ObjectResponse<HttpException>))]
        public ActionResult ListBots(string token)
        {
            if (!MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.ADMINISTRATOR))
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json(new ObjectResponse<string>()
                {
                    Success = false,
                    Data = ServerStringResponse.InvalidCredential
                }, MainClass.serializerOptions);
            }
            return Json(new ObjectResponse<object>()
            {
                Success = true,
                Data = new object()
            }, MainClass.serializerOptions);
        }
    }
}
