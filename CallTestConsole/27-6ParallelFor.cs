using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallTestConsole
{
    public class _27_6ParallelFor
    {
        int totalSize = 0;
        List<int> sourcedata = new List<int>();
        public void BeginWork()
        {
            int size = DirectorySize();
            Console.WriteLine("dir size:" + size);
            // GovStopDemo();

            // GovForEachDemo();

            Console.ReadLine();
        }


        void GovForEachDemo()
        {
            // The sum of these elements is 40.
            int[] input = { 4, 1, 6, 2, 9, 5, 10, 3 };
            int sum = 0;

            try
            {
                Parallel.ForEach(
                        input,
                        () =>
                        {
                            Console.WriteLine("初始化任务；当前任务所在线程ID：" + Thread.CurrentThread.ManagedThreadId);
                            return 0;
                        },                            // thread local initializer
                        (n, loopState, localSum) =>        // body
                    {
                        localSum += n;
                        return localSum;
                    },
                        (localSum) =>
                        {
                            Interlocked.Add(ref sum, localSum);
                            Console.WriteLine("结束任务");
                        }
                    );

                Console.WriteLine("\nSum={0}", sum);
            }
            // No exception is expected in this example, but if one is still thrown from a task,
            // it will be wrapped in AggregateException and propagated to the main thread.
            catch (AggregateException e)
            {
                Console.WriteLine("Parallel.ForEach has thrown an exception. THIS WAS NOT EXPECTED.\n{0}", e);
            }

        }

        /// <summary>
        /// 目录的文件总大小
        /// 考察：3委托版本的综合使用
        /// </summary>
        /// <returns></returns>
        int DirectorySize()
        {
            string directorPath = "D:\\workdir";
            IEnumerable<string> allfiles = Directory.EnumerateFiles(directorPath);

            Parallel.ForEach(allfiles,
                (tsource) =>
                {
                    FileStream fs = File.OpenRead(tsource);
                    totalSize += (int)fs.Length;
                    Console.WriteLine("{0}大小：{1} B,；线程ID:{2}", fs.Name, fs.Length, Thread.CurrentThread.ManagedThreadId);
                });
            return totalSize;
        }

        #region MSDN Stop Demo
        /// <summary>
        /// MSDN demo
        /// 
        /// 实现的效果：循环中，某个循环可以中止所有循环
        /// </summary>
        void GovStopDemo()
        {
            var rnd = new Random();
            long stopIndex = rnd.Next(1, 11);

            Console.WriteLine("Will call Stop in iteration {0}\n", stopIndex);



            var result = Parallel.For(1, 10000, (i, state) =>
            {
                Console.WriteLine("Beginning iteration {0}", i);
                int delay;
                Monitor.Enter(rnd);
                delay = rnd.Next(1, 1001);
                Monitor.Exit(rnd);
                Thread.Sleep(delay);
                if (i == stopIndex)
                {
                    Console.WriteLine("Stop in iteration {0}", i);
                    state.Stop();
                    return;//立即返回，中止此循环
                }

                if (state.IsStopped)
                {
                    Console.WriteLine("此任务{0}已经开始，但是前一刻被stop掉", i);
                    return;//TODO:有可能其它任务已经执行到此，如果不加此代码，那么其它任务会继续执行完。而不是立即被Stop掉。
                }
                Console.WriteLine("Completed iteration {0}", i);
            });
        }
        #endregion
    }
}
