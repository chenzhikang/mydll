using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallTestConsole
{
    public class _24_4CancellationToken
    {
        CancellationTokenSource cts;
        public void Go()
        {
              cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(childProcess);
            Thread.Sleep(3000);//主线程等待一会
            cts.Cancel();//调用取消操作
            Console.ReadLine();
        }
        private void childProcess(object o)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (cts.IsCancellationRequested)
                    break;
                Console.WriteLine("child thread is executing.count" + i);
                Thread.Sleep(100);
            }
            Console.WriteLine("main thread process over");
        }
    }
}
