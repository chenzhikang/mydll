using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{
    public class MyJob2 : IJob
    {
        public void Execute()
        {
            Console.WriteLine("Task2正在执行``` " + DateTime.Now);
        }
        public void HandleException(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}