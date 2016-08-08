using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mydll
{
    public class HttpHelper
    {

        public static string ConvertFullHttpUrl(string oldurl,string domain )
        {
            if (oldurl.StartsWith("/"))
            {
                return "http://"+domain+oldurl;
            }
            oldurl = oldurl.StartsWith("http://") || oldurl.StartsWith("https://") ? oldurl : "http://" + oldurl;
            return oldurl;
        }
        /// <summary>
        /// /xxxx/xxxx/xxx.html
        /// child.domain.com/xxx/a.html
        /// 判断域名 并转换为完整格式
        /// http://xxxx.xxx.xx
        /// </summary>
        /// <param name="oldurl"></param>
        /// <param name="domain">域名 www.aaa.com|aaa.com</param>
        /// <returns></returns>
        public static bool FilterAndConvertReleateToFullUrl(string oldurl, string domain, bool includeSubdomain = true)
        {
            if (oldurl.StartsWith("/"))
            {
                return true;
            }
            //匹配出域名
            Match match = Regex.Match(oldurl, "(\\w+\\.)?(\\w+\\.)?\\w+?\\.\\w{2,4}/");
            if (match.Success)
            {
                string matcheddomain = match.Value;
                if (includeSubdomain)
                {
                    if (matcheddomain.Contains(domain))
                    {
                        return true;
                    }
                }
                else {
                    return domain == matcheddomain + "/" ? true : false;
                }

            }
            return false;
            //比较
        }
        public static string GetHtml(string TargetUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(TargetUrl);
            request.Method = "Get";
            request.ContentType = "text/xml";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


    }
}
