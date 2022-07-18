using Microsoft.AspNetCore.Mvc;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        [HttpGet]
        [Route("{hash}")]
        public ActionResult FetchFilesFromHash(string hash)
        {
            var returnContent = new List<PublishedReleaseFile>();
            if (MainClass.contentManager.Published.ContainsKey(hash))
            {
                var commit = MainClass.contentManager.Published[hash];
                returnContent = new List<PublishedReleaseFile>(commit.Files);
            }
            return Json(returnContent, MainClass.serializerOptions);
        }
    }
}
