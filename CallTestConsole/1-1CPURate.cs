using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
namespace CallTestConsole
{
    /// <summary>
    /// 
    /// </summary>
    public class _1_1CPURate
    {
        int rangeTime = 200;
        double angleRate = 0.05;
        int _cpus = 1;
        public _1_1CPURate(int cpus)
        {
            _cpus = cpus;
        }
        public void BeginWork()
        {
            for (int i = 0; i < _cpus; i++)
            {
                ThreadPool.QueueUserWorkItem(computeSingleCPU);
            }
        }
        void computeSingleCPU(object obj)
        {
            double cpuRate = 0;
            double executetime = 0, sleeptime;
            
            var watch = Stopwatch.StartNew();
            while (true)
            {
                for (double angle = 0; angle < 2 * Math.PI; angle += angleRate)
                {
                    cpuRate = Math.Sin(angle);
                    executetime = cpuRate * rangeTime;
                    sleeptime =  (rangeTime - executetime);
                    while (watch.ElapsedMilliseconds < executetime)
                    {
                    }
                    Thread.Sleep((int)sleeptime);                    
                    watch.Restart();
                }
            }
        }
    }
}
