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
            var contentManager = MainClass.contentManager;
            if (contentManager != null)
            {
                if (contentManager.Published.ContainsKey(hash))
                {
                    var commit = contentManager.Published[hash];
                    returnContent = new List<PublishedReleaseFile>(commit.Files);
                }
                contentManager.DatabaseSerialize();
            }
            return Json(returnContent, MainClass.serializerOptions);
        }
    }
}
