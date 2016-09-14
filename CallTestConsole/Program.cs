using CallTestConsole.Tasks;
using mydll;
using MyDllCollection;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Timers;
namespace CallTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("begin to compute");

           

            Console.Read();
        }

    }
    public class Clock
    {
        private static Clock _instance = new Clock();
        private System.Timers.Timer _timer = new System.Timers.Timer(1000);
        private Clock()
        {
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += TimerHandler;
        }
        /// <summary>
        /// 启动时钟
        /// </summary>
        public void StartClock()
        {
            _timer.Start();
        }
        public static Clock Instance
        {
            get
            {
                return _instance;
            }
        }
        private void TimerHandler(object sender, ElapsedEventArgs e)
        {
            DateTime signalTime = e.SignalTime;
            Console.WriteLine(signalTime.ToString("mmss.fff"));
        }
    }

}

