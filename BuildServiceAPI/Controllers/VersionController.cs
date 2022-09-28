using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    public class VersionController : Controller
    {
        [HttpGet("/version")]
        public string ServerVersion()
        {
            var asm = Assembly.GetAssembly(typeof(VersionController));
            if (asm == null)
            {
                return @"0.0.0.0";
            }

            Version? version = asm.GetName().Version;
            
            if (version == null)
            {
                return @"0.0.0.0";
            }

            return version.ToString();
        }

        [HttpGet("/uptime")]
        public long ServerUptime()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - MainClass.StartupTimestamp;
        }
    }
}
