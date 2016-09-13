using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace CallTestConsole
{
    public class FalseShare
    {
        //强制分开：4703、4665
        //伪共享冲突时：7667、9708、7850
        private int count = 1000000000;//10亿
        int operations = 2;//
        public void BeginWork()
        {
            Data data = new Data();

            ThreadPool.QueueUserWorkItem(o => workmember(data, true));
            ThreadPool.QueueUserWorkItem(o => workmember(data, false));

            Console.ReadLine();
        }

        private void workmember(Data data, bool isfirst)
        {
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                if (isfirst)
                    data.field1++;
                else
                    data.field2++;
            }
            if (Interlocked.Decrement(ref operations) == 0)
            {
                watch.Stop();
                Console.WriteLine("program is over! time:" + watch.ElapsedMilliseconds);
            }
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public class Data
    {
        [FieldOffset(0)]
        public int field1;
        [FieldOffset(64)]
        public int field2;
    }
    //public class Data
    //{
    //    public int field1;
    //    public int field2;
    //}
}
