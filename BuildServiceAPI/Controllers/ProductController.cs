using Microsoft.AspNetCore.Mvc;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("available")]
        public ActionResult FetchAvailable() => FetchAvailable("");

        public ActionResult FetchAvailable(string token)
        {
            var productIDList = new List<string>();

            return Json(productIDList, MainClass.serializerOptions);
        }
    }
}
