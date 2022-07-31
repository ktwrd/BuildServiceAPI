using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuildServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public string Grant(string username, string password)
        {
            var isValid = false;
            var validatorSignature = "";
            foreach (var validator in MainClass.TokenGrantList)
            {
                var res = validator.Grant(username, password);
                if (res)
                {
                    isValid = true;
                    validatorSignature = validator.GetType().ToString();
                    break;
                }
            }

            if (!isValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return "";
            }

            return MainClass.RegisterToken(validatorSignature, username, password);
        }
    }
}
