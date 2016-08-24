using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{

    public class MyJob1 : IJob
    {
        public JobConfiguration Config
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Execute(object sender)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("开始执行任务···");
            Console.WriteLine("正在执行任务···");
            Console.WriteLine("结束执行任务···");
            Console.WriteLine();
        }

        public void HandleException(object sender)
        {
            //log
        }
    }
}
