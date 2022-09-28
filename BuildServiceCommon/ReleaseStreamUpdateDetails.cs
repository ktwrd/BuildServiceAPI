using BuildServiceCommon.AutoUpdater;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon
{
    public class ReleaseStreamUpdateDetails
    {
        /// <summary>
        /// Nullable
        /// </summary>
        public ProductReleaseStream Stream { get; set; } = null;

        public string TargetApp { get; set; } = "";
        public string TargetBranch { get; set; } = "";
        public string CurrentHash { get; set; } = "";

        public long UpdateReleaseTimestamp { get; set; } = -1;
        public bool HasUpdate { get; set; } = false;
    }
}
