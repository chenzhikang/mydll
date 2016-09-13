using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDllCollection
{
  public  class UrlHelper
    {
        public static string ParseParameter(string url, string pname, StringComparison comparison) {
            Dictionary<string, string> dics = Parse(url);//todo:重写字符串比较器，忽略key的大小写
            if (dics.ContainsKey(pname))
            {
                return dics[pname];
            }
            return string.Empty;
        }
        private static Dictionary<string, string> Parse(string url)
        {
            //http://shop.m.taobao.com/shop/coupon.htm?seller_id=906289606&activity_id=a1c16f4415cf42c3b579f3e04dbadc7b

            Dictionary<string, string> dics = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            int indexof = url.LastIndexOf('?');
            if (indexof < 0)
            {
                return dics;
            }
            string pstr = url.Substring(indexof + 1);
            string[] ppairs = pstr.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in ppairs)
            {
                string[] strs = pair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                dics.Add(strs[0], strs[1]);
            }
            return dics;
        }
    }
}
