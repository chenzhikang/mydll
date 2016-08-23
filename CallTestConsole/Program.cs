using fav._6655.com;
using MyDllCollection;
using System;
using System.Diagnostics;
using System.Text;

namespace CallTestConsole
{
    class Program
    {
        private const int count = 1000;
        static void Main(string[] args)
        {
            

            Console.WriteLine("开始测log4net");
            Logging.LogInfo(typeof(Program), "开始测log4net");

            Console.WriteLine("over");
            Console.Read();
        }
    }
}
