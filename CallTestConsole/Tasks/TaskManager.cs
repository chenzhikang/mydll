using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace CallTestConsole.Tasks
{
    public class TaskManager
    {
        private static TaskManager _taskManager = new TaskManager();//要求是单例的
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();//只能在实例化此类的时候赋值
        private TaskManager()
        {

        }
        /// <summary>
        /// 用于创建计划人物并初始化任务
        /// 比如程序写死、从数据库读取人物内容和配置信息，然后实例化。
        /// </summary>
        public void CreateAllTaskthread()
        {
            //清空
            _taskThreads.Clear();

            #region 程序写死的任务
            IJob job1 = new MyJob1();
                   
            JobConfiguration config = new JobConfiguration
            {
                AllowLoop = true,
                Enabled = true,
                Seconds = 1,
                StopOnException = true
            };
            Task task1 = new Task(job1, config);

            TaskThread th1 = new TaskThread(task1);



            #endregion

            #region 通过 数据库持久化的任务配置信息 来实例化Task对象

            #endregion

            _taskThreads.Add(th1);
        }
        /// <summary>
        /// 启动任务线程
        /// </summary>
        public void StartAllthread()
        {
            foreach (var th in _taskThreads)
            {
                th.InitStartTimer();
            }
        }

        /// <summary>
        /// 停止所有任务线程
        /// </summary>
        public void StopAllthread()
        {
            foreach (var th in _taskThreads)
            {
                th.Dispose();
            }
        }
        /// <summary>
        /// 单实例
        /// </summary>
        public static TaskManager Instance
        {
            get
            {
                return _taskManager;
            }
        }
        /// <summary>
        /// 当前的任务线程集合
        /// </summary>
        public IList<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(_taskThreads);
            }
        }
    }
}
