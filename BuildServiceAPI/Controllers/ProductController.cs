using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;
using System.Diagnostics;

namespace BuildServiceAPI.Controllers
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> FetchAll();
        IQueryable<TEntity> Query { get; }
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Save();
    }

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<PublishController> _logger;

        public ProductController(ILogger<PublishController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public string Get(string id)
        {
            return id;
        }
    }
}
