﻿using Microsoft.AspNetCore.Mvc;
using Minalyze.Shared.AutoUpdater;
using System.Text.Json;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReleaseController : Controller
    {
        [HttpGet]
        public ActionResult Index(string token)
        {
            if (!MainClass.ValidTokens.Contains(token))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            return Json(MainClass.AvailableReleases, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("latest/{app}")]
        public ActionResult Latest(string app)
        {
            var returnContent = new List<ProductRelease>();
            if (MainClass.AvailableReleases.ContainsKey(app))
            {
                var toMap = new Dictionary<string, List<ReleaseInfo>>();
                foreach (var release in MainClass.ReleaseInfoContent)
                {
                    if (!toMap.ContainsKey(release.remoteLocation))
                        toMap.Add(release.remoteLocation, new List<ReleaseInfo>());
                    toMap[release.remoteLocation].Add(release);
                }

                var sortedDictionary = new Dictionary<string, List<ReleaseInfo>>();
                foreach (var pair in toMap)
                {
                    sortedDictionary.Add(pair.Key, pair.Value.OrderBy(o => o.envtimestamp).Reverse().ToList());
                }

                var latestOfAll = new List<ReleaseInfo>();
                foreach (var pair in sortedDictionary)
                {
                    latestOfAll.Add(pair.Value.First());
                }

                foreach (var pair in MainClass.TransformReleaseList(latestOfAll.ToArray()))
                {
                    returnContent.Add(pair.Value);
                }
            }
            return Json(returnContent, MainClass.serializerOptions);
        }
    }
}