using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mydll;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections;
using System.Threading;

namespace CallTestConsole
{
    class Program
    {

        /*
        1种子网址
        2获取所有链接
        3判断是本站链接
        4保存到带访问队列
        5按照图广度优先算法进行爬去
        6 重复
            */
        static void Main(string[] args)
        {
            string domain = "cnblogs.com";
            HashSet<string> visitedList = new HashSet<string>();
            Queue<string> unvisitedQueue = new Queue<string>();
            unvisitedQueue.Enqueue("http://www.cnblogs.com");//种子队列http://www.cnblogs.com/
            while (unvisitedQueue.Count > 0)
            {
                string topurl = unvisitedQueue.Dequeue();
                visitedList.Add(topurl);  //放到已访问表中

                topurl = HttpHelper.ConvertFullHttpUrl(topurl, domain);

                string originHtmlcode = HttpHelper.GetHtml(topurl);

                List<string> urls = RegexHelper.ParseAllLinkUrl(originHtmlcode);

                foreach (string newUrl in urls)
                {
                    #region    判断是否为本域名
                    bool IsThisSiteDomain = HttpHelper.FilterAndConvertReleateToFullUrl(newUrl, domain);
                    if (!IsThisSiteDomain)
                    {
                        continue;
                    }
                    #endregion

                    if (visitedList.Contains(newUrl))
                        continue;
                    unvisitedQueue.Enqueue(newUrl);//保存的是直接解析出来的网址，包含相对路径的网址/xxx/xxx.html
                    Console.WriteLine(newUrl);//打印出新url


                }
                // Thread.Sleep(100);
            }

            Console.Read();
        }
    }
}
