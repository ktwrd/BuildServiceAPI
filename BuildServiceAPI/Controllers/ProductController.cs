using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var contentManager = MainClass.contentManager;
            if (contentManager != null)
            {
                foreach (var pair in contentManager.Published)
                {
                    if (!productIDList.Contains(pair.Value.Release.appID))
                        productIDList.Add(pair.Value.Release.appID);
                }
            }

            return Json(productIDList, MainClass.serializerOptions);
        }
    }
}
