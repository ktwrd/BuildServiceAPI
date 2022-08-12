using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon
{
    public class ObjectResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string DataType => Data?.GetType().FullName;
    }
}
