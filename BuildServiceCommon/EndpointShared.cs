﻿using BuildServiceCommon.Authorization;
using BuildServiceCommon.AutoUpdater;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon
{
    public enum SearchMethod
    {
        Equals,
        StartsWith,
        EndsWith,
        Includes,
        IncludesNot
    }
    public enum DataType
    {
        ReleaseInfoArray,
        ReleaseDict,
        PublishDict,
        All
    }
    public class ContentJSON
    {
        public List<ReleaseInfo> ReleaseInfoContent;
        public Dictionary<string, ProductRelease> Releases;
        public Dictionary<string, PublishedRelease> Published;
    }
    public class DataJSON
    {
        public ContentJSON Content;
        public SystemAnnouncementSummary SystemAnnouncement;
        public List<Account> Account;
    }
    public class AllDataResult
    {
        public List<ReleaseInfo> ReleaseInfoContent { get; set; }
        public Dictionary<string, ProductRelease> Releases { get; set; }
        public Dictionary<string, PublishedRelease> Published { get; set; }
    }
}
