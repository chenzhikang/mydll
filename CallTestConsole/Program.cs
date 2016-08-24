using fav._6655.com;
using MyDllCollection;
using System;
using System.Diagnostics;
using System.Text;
using System.Timers;
namespace CallTestConsole
{
    class Program
    {
        private const int count = 1000;
        static void Main(string[] args)
        {
            myTask job1 = new myTask(new JobConfiguration
            {
                AllowLoop = true,
                Interval = 2000,
                StopOnException = true
            });
            myTask2 task2 = new myTask2(new JobConfiguration
            {
                AllowLoop = false,
                Interval = 1000,
                StopOnException = false
            });

            JobExecutor jexecutor = new JobExecutor(job1);
            JobExecutor jexecutor2 = new JobExecutor(task2);
            jexecutor.Start();
            jexecutor2.Start();


            Console.WriteLine("over");
            Console.Read();
        }
    }
    

    /// <summary>
    /// 任务接口
    /// </summary>
    /// 

  

    /// <summary>
    /// 来根据config决定定时的具体实现，并调用回调方法
    /// </summary>

}

