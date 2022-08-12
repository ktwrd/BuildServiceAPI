﻿using Microsoft.AspNetCore.Mvc;
using BuildServiceCommon.AutoUpdater;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using BuildServiceCommon;
using BuildServiceCommon.Authorization;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReleaseController : Controller
    {
        [HttpGet]
        public ActionResult Index(string token)
        {
            if (!MainClass.ValidTokens.ContainsKey(token) || MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.READ_RELEASE_BYPASS))
            {
                Response.StatusCode = 401;
                return Json(new HttpException(401, @"Invalid token"), MainClass.serializerOptions);
            }
            return Json(MainClass.contentManager?.Releases ?? new object(), MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("latest/{app}")]
        public ActionResult LatestFromPath(string app, string token = "")
        {
            var returnContent = new List<ProductRelease>();
            var allowFetch = true;
#if BUILDSERVICEAPI_APP_WHITELIST
            if (app == "com.minalyze.minalogger")
            {
                if (token.Length < 1 || !MainClass.UserByTokenHasService(token, "ml2"))
                {
                    allowFetch = false;
                }
            }
#endif
            bool showExtraBuilds = MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.READ_RELEASE_BYPASS);
            if (allowFetch && (MainClass.contentManager?.Releases.ContainsKey(app) ?? false))
            {
                var toMap = new Dictionary<string, List<ReleaseInfo>>();
                foreach (var release in MainClass.contentManager.ReleaseInfoContent)
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

                var latestOfAllArray = latestOfAll.ToArray();
                foreach (var pair in ReleaseHelper.TransformReleaseList(latestOfAllArray))
                {
                    var rel = new ProductRelease()
                    {
                        ProductID = pair.Value.ProductID,
                        ProductName = pair.Value.ProductName
                    };
                    var tmpStreams = pair.Value.Streams;
                    var streams = new List<ProductReleaseStream>();
                    foreach (var s in tmpStreams)
                    {
                        if (s.BranchName == "Other")
                        {
                            if (showExtraBuilds)
                                streams.Add(s);
                        }
                        else
                            streams.Add(s);

                    }
                    rel.Streams = streams.ToArray();
                    returnContent.Add(rel);
                }
            }
            return Json(returnContent, MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("latest")]
        public ActionResult LatestFromParameter(string id) => LatestFromPath(id);
    }
}
