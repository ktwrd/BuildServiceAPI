using BuildServiceAPI.Minalyze;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildServiceCommon.Authorization
{
    public class AccountManager
    {
        public List<Account> AccountList = new List<Account>();

        public static List<ITokenGranter> TokenGranters = new List<ITokenGranter>()
        {
            new MinaloggerTokenGrant()
        };
        public static void RegisterTokenGranter(ITokenGranter granter)
        {
            foreach (var item in TokenGranters)
                if (item == granter)
                    return;
            TokenGranters.Add(granter);
        }

        public bool IsPendingWrite { get; private set; }
        public event VoidDelegate PendingWrite;
        internal void OnPendingWrite()
        {
            IsPendingWrite = true;
            if (PendingWrite != null)
                PendingWrite?.Invoke();
        }
        public void ClearPendingWrite()
        {
            foreach (var item in AccountList)
                item.ClearPendingWrite();
            IsPendingWrite = false;
        }

        public bool ValidateToken(string token)
        {
            foreach (var account in AccountList)
            {
                foreach (var item in account.Tokens)
                    if (item.Token == token)
                        return true;
            }
            return false;
        }

        public Account GetAccount(string token)
        {
            foreach (var account in AccountList)
            {
                if (account.HasToken(token))
                    return account;
            }
            return null;
        }

        public GrantTokenResponse CreateToken(Account account)
        {
            bool accountFound = false;
            foreach (var item in AccountList)
            {
                if (item == account)
                {
                    accountFound = true;
                    if (item.Enabled)
                    {
                        var token = new AccountToken(item);
                        AccountToken success = item.AddToken(token);
                        if (success == null)
                            return new GrantTokenResponse("Failed to grant token", false);
                        else
                        {
                            if (!IsPendingWrite)
                                OnPendingWrite();
                            return new GrantTokenResponse("Granted token", true, success);
                        }
                    }
                    return new GrantTokenResponse("Account Disabled", false);
                }
            }

            if (!accountFound)
                return new GrantTokenResponse("Account not found", false);
            return new GrantTokenResponse("Failed to grant token", false);
        }

        private GrantTokenResponse AttemptGrantToken(Account account, ITokenGranter granter, string username, string password)
        {
            var success = granter.Grant(username, password);
            if (success)
            {
                return CreateToken(account);
            }
            return null;
        }
        public GrantTokenResponse GrantToken(Account account, string username, string password)
        {
            var taskList = new List<Task>();
            GrantTokenResponse targetResponse = null;
            foreach (var granter in TokenGranters)
            {
                taskList.Add(new Task(delegate
                {
                    var res = AttemptGrantToken(account, granter, username, password);
                    if (targetResponse == null && res != null)
                        targetResponse = res;
                }));
            }
            foreach (var i in taskList)
                i.Start();
            Task.WhenAll(taskList).Wait();
            if (targetResponse == null)
                return new GrantTokenResponse("Failed to grant token", false);
            return targetResponse;
        }

        public GrantTokenResponse GrantTokenAndOrAccount(string username, string password)
        {
            foreach (var account in AccountList)
            {
                // Found our account
                if (account.Username == username)
                {
                    return GrantToken(account, username, password);
                }
            }

            // Create account
            var accountInstance = new Account(this);
            accountInstance.Username = username;
            AccountList.Add(accountInstance);
            var response = GrantToken(accountInstance, username, password);
            if (!response.Success)
                AccountList.Remove(accountInstance);
            return response;
        }

        public void Read(string jsonContent)
        {
            if (jsonContent.Length < 1)
                Trace.WriteLine($"[AccountManager->Read:{GeneralHelper.GetNanoseconds()}] [ERR] Argument jsonContent has invalid length of {jsonContent.Length}");
            if (!RegExStatements.JSON.Match(jsonContent).Success)
                Trace.WriteLine($"[AccountManager->Read:{GeneralHelper.GetNanoseconds()}] [ERR] Argument jsonContent failed RegExp validation\n--------\n{jsonContent}\n--------\n");
            var jsonOptions = new JsonSerializerOptions()
            {
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false,
                IncludeFields = true
            };
            var deserialized = JsonSerializer.Deserialize<List<Account>>(jsonContent, jsonOptions);
            if (deserialized == null)
            {
                Trace.WriteLine($"[AccountManager->Read:{GeneralHelper.GetNanoseconds()}] [WARN] Deserialized List<Account> is null. Content is\n--------\n{jsonContent}\n--------\n");
            }
            foreach (var item in deserialized)
            {
                if (item.accountManager == null)
                    item.accountManager = this;
            }
            AccountList = deserialized;
            Trace.WriteLine($"[AccountManager->Read:{GeneralHelper.GetNanoseconds()}] Deserialized Accounts ({AccountList.Count} Accounts)");
        }

        public string ToJSON()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                IgnoreReadOnlyFields = false,
                IgnoreReadOnlyProperties = false,
                IncludeFields = true,
                WriteIndented = true
            };
            try
            {
                var serialized = JsonSerializer.Serialize(AccountList, jsonOptions);
                return serialized;
            }
            catch (Exception except)
            {
                string txt = $"[AccountManager->Read:{GeneralHelper.GetNanoseconds()}] [ERR] Failed to serialize field AccountList\n--------\n{except}\n--------\n";
                Trace.WriteLine(txt);
                Console.Error.WriteLine(txt);
                throw;
            }
        }
    }
}
