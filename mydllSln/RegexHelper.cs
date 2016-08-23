using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace MyDllCollection
{
    public class RegexHelper
    {
        /// <summary>
        /// 匹配所有的href超链接
        /// </summary>
        /// <param name="sourceHtmlcode"></param>
        /// <returns></returns>
        public static List<string> ParseAllLinkUrl(string sourceHtmlcode)
        {
            List<string> urls = new List<string>();
            Regex regex = new Regex("href=[\"'](\\S*?)[\"']", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(sourceHtmlcode);
            if (matches.Count == 0)
                return urls;
            foreach (Match match in matches)
            {
                if (match.Groups.Count == 2)
                {
                    string url = match.Groups[1].Value;

                    if (url.StartsWith("#", StringComparison.CurrentCultureIgnoreCase) || url.StartsWith("javascript", StringComparison.CurrentCultureIgnoreCase))
                    { }
                    else {

                        urls.Add(url);
                    }

                }
            }


            #region 移除 link的href链接
            var linkhref = FindPatternValueOne(sourceHtmlcode, "<link[\\s\\S]*?href=[\"'](\\S+?)[\"']");

            foreach (var linkhrefitem in linkhref)
            {
                urls.Remove(linkhrefitem);
            }
            #endregion

            return urls;
        }
        private static List<string> FindPatternValueOne(string sourcehtmlcode, string pattern)
        {//
            List<string> result = new List<string>();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(sourcehtmlcode);
            if (matches.Count == 0)
                return result;
            foreach (Match match in matches)
            {
                Group group = match.Groups.Count == 2 ? match.Groups[1] : null;
                if (group != null)
                    result.Add(group.Value);
            }
            return result;
        }
        public static string ParseHtmlCodeToText(string sourcehtmlcode, bool removespace = true)
        {
            StringBuilder parsedtext = new StringBuilder();
            Regex reg = new Regex(@">([\s\S]*?)<", RegexOptions.IgnoreCase);
            var matches = reg.Matches(sourcehtmlcode);
            if (matches == null || matches.Count == 0)
            {
                return "";
            }
            foreach (Match match in matches)
            {
                if (!match.Success)
                {
                    continue;
                }
                bool rightGroup = match.Groups.Count == 2 && match.Groups[1] != null;
                if (!rightGroup)
                    continue;
                string value = match.Groups[1].Value;
                if (string.IsNullOrEmpty(value))
                    continue;
                parsedtext.Append(value);
            }
            parsedtext.Replace(" ", "");
            return parsedtext.ToString();
        }
    }
}
