using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{
    public class MyJob1 : IJob
    {
        int i = 0;

        public void Execute()
        {
            i++;
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("开始执行任务···");
            Console.WriteLine("正在执行任务···" + i);
            Console.WriteLine("结束执行任务···");
            Console.WriteLine();
            if (i > 2)
                throw new NotImplementedException();
        }
        public void HandleException(Exception ex)
        {
            //log
        }
    }
}