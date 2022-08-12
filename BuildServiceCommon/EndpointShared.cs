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
}
