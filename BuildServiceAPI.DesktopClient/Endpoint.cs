using BuildServiceCommon;
using kate.shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildServiceAPI.DesktopClient
{
    internal class Endpoint
    {
        internal static string Base
        {
            get
            {
                return (string)Properties.Settings.Default["ServerEndpoint"]; ;
            }
            set
            {
                Properties.Settings.Default["APIEndpoint"] = value;
                OnBaseAPIChanged(value);
            }
        }
        internal static event StringDelegate BaseAPIChanged;
        internal static void OnBaseAPIChanged(string route)
        {
            BaseAPIChanged?.Invoke(route);
        }
        private static string encode(string content)
            => WebUtility.UrlEncode(content);
        private static string encode(dynamic content)
            => encode(content.ToString());
        internal static string LatestReleaseBase
            => $"{Base}/release/latest";
        internal static string LatestRelease(string id)
            => $"{LatestReleaseBase}/{id}";
        internal static string LatestRelease(string id, string token)
            => $"{LatestReleaseBase}/{id}?token={encode(token)}";
        internal static string Files(string hash)
            => $"{Base}/file?hash={encode(hash)}";
        internal static string AvailableProducts()
            => $"{Base}/products/available";
        internal static string AvailableProducts(string token)
            => $"{AvailableProducts()}?token={encode(token)}";

        internal static string TokenGrant(string username, string password)
            => $"{Base}/token/grant?username={encode(username)}&password={encode(password)}";
        internal static string TokenValidate(string token)
            => $"{Base}/token/validate?token={encode(token)}";
        internal static string TokenDetails(string token)
            => $"{Base}/token/details?token={encode(token)}";
        internal static string TokenRemove(string token, bool all = false)
            => $"{Base}/token/remove?token={encode(token)}&all={encode(all)}";

        internal static string AnnouncementLatest()
            => $"{Base}/admin/announcement/latest";
        internal static string AnnouncementCreate(string token, string content, bool active = true)
            => $"{Base}/admin/announcement/new?token={encode(token)}&content={encode(content)}&active={encode(active.ToString())}";
        internal static string AnnouncementUpdateCurrent(string token, string content, bool active = true)
            => $"{Base}/admin/announcement/update?token={encode(token)}&content={encode(content)}&active={encode(active.ToString())}";
        internal static string AnnouncementFetchAll(string token)
            => $"{Base}/admin/announcement/all?token={encode(token)}";
        internal static string AnnouncementSetData(string token, SystemAnnouncementSummary summary)
            => $"{Base}/admin/announcement/setData?token={encode(token)}&content={encode(JsonSerializer.Serialize(summary, Program.serializerOptions))}";
        internal static string AnnouncementSummary(string token)
            => $"{Base}/admin/announcement/summary?token={encode(token)}";

        internal static string DumpSetData(string token, DataType type)
            => $"{Base}/admin/data/setdata?token={encode(token)}&type={encode((int)type)}";
        internal static string DumpDataFetch(string token, DataType type)
            => $"{Base}/admin/data/dump?token={encode(token)}&type={encode((int)type)}";
        internal static string UserList(
            string token,
            string username = "",
            SearchMethod usernameSearchMethod = SearchMethod.Equals,
            long firstSeenTimestamp = 0,
            long lastSeenTimestamp = long.MaxValue)
            => $"{Base}/admin/user/list?token={encode(token)}&username={encode(username)}&usernameSearchMethod={encode(usernameSearchMethod)}&firstSeenTimestamp={encode(firstSeenTimestamp)}&lastSeenTimestamp={lastSeenTimestamp}";
    }
}
