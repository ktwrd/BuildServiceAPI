using System;
using System.Collections.Generic;
using System.Text;

namespace BuildServiceCommon.Authorization
{
    public class GrantTokenResponse
    {
        public GrantTokenResponse(string message = "", bool success = false, AccountToken token = null)
        {
            Message = message;
            Token = token;
            Success = success;
        }
        public bool Success { get; private set; }
        public string Message { get; private set; }

        /// <summary>
        /// Nullable
        /// </summary>
        public AccountToken Token { get; private set; }
    }
}
