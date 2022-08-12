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
    public class AllDataResult
    {
        public List<ReleaseInfo> ReleaseInfoContent { get; set; }
        public Dictionary<string, ProductRelease> Releases { get; set; }
        public Dictionary<string, PublishedRelease> Published { get; set; }
    }
}
