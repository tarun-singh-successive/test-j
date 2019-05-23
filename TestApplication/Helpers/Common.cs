using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TestApplication.DataLayer;
using TestApplication.Entities;

namespace TestApplication.Helpers
{
    public class Common
    {
        public static int PageSize = 25;

        public static string BaseUrl
        {
            get
            {
                var request = HttpContext.Current.Request;
                return request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Host + (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port) + "/";
            }
        }

        public static string GetUrl(string action, string controller)
        {
            return action == null
                ? string.Format("{0}{1}", BaseUrl, controller)
                : string.Format("{0}{1}/{2}", BaseUrl, controller, action);
        }

        public static string GetUrl(string action, string controller, dynamic id)
        {
            return id == null
                ? string.Format("{0}/{1}/{2}", BaseUrl, controller, action)
                : string.Format("{0}/{1}/{2}/{3}", BaseUrl, controller, action, id);
        }

        public static string GetGridFooterString(int totalCoumn, int totalRecords)
        {
            return string.Format("<td colspan='{0}' class='pager'><span class='pull-left label label-primary'> Total Records : {1}</span>", totalCoumn, totalRecords);
        }

        public static IList<KeyValuePair<string, string>> ToKeyValuePairs(string uri, params string[] excepts)
        {
            if (uri == null)
            {
                return new List<KeyValuePair<string, string>>();
            }
            if (!uri.StartsWith("?"))
            {
                uri = $"?{uri}";
            }
            var matches = Regex.Matches(uri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            return matches.Cast<Match>()
                .Select(m => new KeyValuePair<string, string>(Uri.UnescapeDataString(m.Groups[2].Value?.ToLower()), Uri.UnescapeDataString(m.Groups[3].Value?.ToLower())))
                .Where(x => excepts == null || !excepts.Contains(x.Key)).ToList();
        }

        public static string UpdateQueryString(string uri, string key, string value)
        {
            var queryParams = ToKeyValuePairs(uri, key);
            queryParams.Add(new KeyValuePair<string, string>(key, value));
            return ToQueryString(queryParams);
        }

        public static string ToQueryString(IEnumerable<KeyValuePair<string, string>> list)
        {
            return string.Join("&", list.Select(x => $"{x.Key}={x.Value}"));
        }

        private static Random random = new Random();
        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string UniqueTransactionId()
        {
            var str = RandomString();
            while (Repository.Instance.Find<AmountTransaction>(x => x.Id == str) != null)
            {
                str = RandomString();
            }
            return str;
        }
    }
}