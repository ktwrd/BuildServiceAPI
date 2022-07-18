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
            if (MainClass.CommitFileTable.ContainsKey(hash))
            {
                var commit = MainClass.CommitFileTable[hash];
                returnContent = new List<PublishedReleaseFile>(commit.Files);
            }
            return Json(returnContent, MainClass.serializerOptions);
        }
    }
}
