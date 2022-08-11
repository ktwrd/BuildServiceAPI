using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BuildServiceCommon.Authorization
{
    public enum AccountPermission
    {
        ADMINISTRATOR,
        CREATE_RELEASE,
        READ_RELEASE_BYPASS,
        DELETE_RELEASE,
        DELETE_ACCOUNT,
        CONFIG_READ,
        CONFIG_WRITE
    }
    public class AccountDisableReason
    {
        public string Message = "";
        public long Timestamp = 0;
    }
    public class AccountTokenDetailsResponse
    {
        public string Username { get; set; }
        public bool Enabled { get; set; }
        public long CreatedTimestamp { get; set; }
    }
    public class Account
    {
        internal AccountManager accountManager = null;
        public string Username { get; set; }
        public List<AccountToken> Tokens { get; set; }
        public List<AccountPermission> Permissions { get; set; }
        public Account(AccountManager accountManager)
        {
            this.accountManager = accountManager;

            Username = "";
            PendingWrite = false;
            Tokens = new List<AccountToken>();
            Permissions = new List<AccountPermission>();
            Enabled = true;
        }
        public Account() : this(null)
        { }

        public bool Enabled { get; set; }
        public List<AccountDisableReason> DisableReasons = new List<AccountDisableReason>();

        private bool _pendingWrite = false;

        /// <summary>
        /// Is there new data that doesn't exist locally.
        /// </summary>
        public bool PendingWrite
        {
            get => _pendingWrite;
            private set
            {
                _pendingWrite = value;
                if (value)
                    accountManager.OnPendingWrite();
            }
        }

        public bool HasToken(string token)
        {
            foreach (var item in Tokens)
            {
                if (item.Token == token)
                    return true;
            }
            return false;
        }

        public AccountTokenDetailsResponse GetTokenDetails(string token)
        {
            foreach (var item in Tokens)
            {
                if (item.Token == token)
                {
                    return new AccountTokenDetailsResponse()
                    {
                        Username = this.Username,
                        Enabled = this.Enabled,
                        CreatedTimestamp = item.CreatedTimestamp
                    };
                }
            }
            return null;
        }
        public void RemoveToken(string token) => RemoveToken(new string[] { token });
        public void RemoveToken(string[] tokens)
        {
            var newTokenList = new List<AccountToken>();
            foreach (var item in Tokens)
            {
                if (!tokens.Contains(item.Token))
                    newTokenList.Add(item);
            }
            Tokens = newTokenList;
            PendingWrite = true;

        }
        public void RemoveTokens()
        {
            Tokens = new List<AccountToken>();
            PendingWrite = true;
        }

        public void DisableAccount(string reason = "No reason")
        {
            Enabled = false;
            DisableReasons.Add(new AccountDisableReason()
            {
                Message = reason
            });
        }

        public void CleanDisableReasons()
        {
            Trace.WriteLine($"[Account->CleanDisableReasons:{GeneralHelper.GetNanoseconds()}] {Username}");
            DisableReasons.Clear();
            PendingWrite = true;
        }

        /// <summary>
        /// Grant to the account a permission if they don't have it already.
        /// </summary>
        /// <param name="target">Permission to add to the user</param>
        /// <returns>If the user has the permission already</returns>
        public bool GrantPermission(AccountPermission target)
        {
            foreach (var perm in Permissions)
                if (perm == target)
                    return true;
            Permissions.Add(target);
            PendingWrite = true;
            return false;
        }

        /// <summary>
        /// Check if the account has a certian permission
        /// </summary>
        /// <param name="target">Permission to look for.</param>
        /// <returns>True if the account has the permission, False if they do not have it.</returns>
        public bool HasPermission(AccountPermission target)
        {
            foreach (var item in Permissions)
                if (item == target)
                    return true;
            return false;
        }

        public AccountToken AddToken(AccountToken targetToken)
        {
            if (targetToken == null) return null;
            if (targetToken.parentAccount != this) return null;
            if (Enabled)
            {
                foreach (var token in Tokens)
                {
                    if (token == targetToken)
                        return token;
                }
                targetToken.CreatedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Tokens.Add(targetToken);
                PendingWrite = true;
                Trace.WriteLine($"[Account->AddToken:{GeneralHelper.GetNanoseconds()}] Granted token for {Username}");
                return targetToken;
            }
            return null;
        }

        internal void ClearPendingWrite()
        {
            _pendingWrite = false;
        }
    }
}
