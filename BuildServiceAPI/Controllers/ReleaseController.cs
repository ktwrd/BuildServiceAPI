using Microsoft.AspNetCore.Mvc;
using BuildServiceCommon.AutoUpdater;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using BuildServiceCommon;
using BuildServiceCommon.Authorization;
using BuildServiceAPI.BuildServiceAPI;
using System.Net;
using System;

namespace BuildServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReleaseController : Controller
    {
        [HttpGet]
        public ActionResult Index(string token)
        {
            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account != null)
            {
                if (ServerConfig.GetBoolean("Security", "AllowPermission_ReadReleaseBypass", true) && account.HasPermission(AccountPermission.READ_RELEASE_BYPASS))
                {
                    return Json(new ObjectResponse<Dictionary<string, ProductRelease>>()
                    {
                        Success = true,
                        Data = MainClass.contentManager?.Releases ?? new Dictionary<string, ProductRelease>()
                    }, MainClass.serializerOptions);
                }
                if (ServerConfig.GetBoolean("Security", "AllowAdminOverride", true) && account.HasPermission(AccountPermission.ADMINISTRATOR))
                {
                    return Json(new ObjectResponse<Dictionary<string, ProductRelease>>()
                    {
                        Success = true,
                        Data = MainClass.contentManager?.Releases ?? new Dictionary<string, ProductRelease>()
                    }, MainClass.serializerOptions);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new ObjectResponse<HttpException>()
            {
                Success = false,
                Data = new HttpException(401, @"Invalid token")
            }, MainClass.serializerOptions);
        }

        private List<ProductRelease> fetchReleasesByAppID(string app, string token)
        {
            var returnContent = new List<ProductRelease>();
            var allowFetch = true;
            bool showExtraBuilds = MainClass.contentManager.AccountManager.AccountHasPermission(token, AccountPermission.READ_RELEASE_BYPASS)
                && ServerConfig.GetBoolean("Security", "AllowPermission_ReadReleaseBypass", true);
            if (allowFetch && (MainClass.contentManager?.Releases.ContainsKey(app) ?? false))
            {
                var toMap = new Dictionary<string, List<ReleaseInfo>>();
                foreach (var release in MainClass.contentManager.ReleaseInfoContent)
                {
                    if (app != release.appID) continue;
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
                        ProductName = pair.Value.ProductName,
                        UID = pair.Value.UID
                    };
                    var tmpStreams = pair.Value.Streams;
                    var streams = new List<ProductReleaseStream>();
                    foreach (var s in tmpStreams)
                    {
                        streams.Add(s);
                    }
                    rel.Streams = streams.ToArray();
                    returnContent.Add(rel);
                }
            }

            var filteredReleases = new List<ProductRelease>();
            var account = MainClass.contentManager.AccountManager.GetAccount(token);
            if (account == null)
            {
                returnContent.Clear();
            }
            else
            {
                foreach (var product in returnContent)
                {
                    var newProduct = new ProductRelease()
                    {
                        ProductName = product.ProductName,
                        ProductID = product.ProductID,
                        UID = product.UID
                    };
                    var filteredStreams = new List<ProductReleaseStream>();
                    foreach (var stream in product.Streams)
                    {
                        bool isOtherBranch = true;
                        switch (stream.BranchName.ToLower())
                        {
                            case "nightly":
                                isOtherBranch = false;
                                break;
                            case "beta":
                                isOtherBranch = false;
                                break;
                            case "stable":
                                isOtherBranch = false;
                                break;
                        }
                        bool allowStream = MainClass.CanUserGroupsAccessStream(stream.GroupBlacklist.ToArray(), stream.GroupWhitelist.ToArray(), account);

                        if (ServerConfig.GetBoolean("Security", "AllowAdminOverride", true))
                        {
                            if (account.HasPermission(AccountPermission.ADMINISTRATOR))
                                allowStream = true;
                        }

                        if (isOtherBranch)
                        {
                            if (ServerConfig.GetBoolean("Security", "AllowPermission_ReadReleaseBypass", true))
                                if (account.HasPermission(AccountPermission.READ_RELEASE_BYPASS))
                                    allowStream = true;
                                else
                                    allowStream = false;
                        }

                        Console.WriteLine($"{stream.RemoteSignature} {allowStream}");

                        if (allowStream)
                            filteredStreams.Add(stream);
                    }
                    newProduct.Streams = filteredStreams.ToArray();
                    filteredReleases.Add(newProduct);
                }
                returnContent = filteredReleases;
            }

            return returnContent;
        }

        [HttpGet]
        [Route("latest/{app}")]
        public ActionResult LatestFromPath(string app, string token = "")
        {
            return Json(fetchReleasesByAppID(app, token), MainClass.serializerOptions);
        }

        [HttpGet]
        [Route("latest")]
        public ActionResult LatestFromParameter(string id="", string token = "")
        {
            // Get all latest available
            if (id.Length < 1)
            {
                // Get list of all AppID's
                var appIDlist = new List<string>();
                foreach (var k in MainClass.contentManager.ReleaseInfoContent)
                    appIDlist.Add(k.appID);
                var resultList = new List<ProductRelease>();
                foreach (var v in appIDlist)
                    resultList = resultList.Concat(fetchReleasesByAppID(v, token)).ToList();
                return Json(resultList.ToArray(), MainClass.serializerOptions);
            }
            else
            {
                return LatestFromPath(id, token);
            }
        }
    }
}
