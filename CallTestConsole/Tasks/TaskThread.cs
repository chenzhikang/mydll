using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{
    /// <summary>
    /// 真正执行任务，包含定时器;只执行一个任务
    /// </summary>
    public class TaskThread : IDisposable
    {
        private int _Interval;
        Timer _timer;
        Task _task;
        public TaskThread(Task task)
        {
            _task = task;
            _Interval = _task.Config.Seconds * 1000;
        }
        /// <summary>
        /// 启动线程定时器
        /// 防止重复启动！！！
        /// </summary>
        public void InitStartTimer()
        {
            if (this._timer == null)
            {
                this._timer = new Timer(this.TimerHandler, null, this._Interval, this._Interval);
            }
           
        }
        /// <summary>
        /// 计时器响应函数
        /// </summary>
        /// <param name="state"></param>
        private void TimerHandler(object state)
        {
            if (_task.Config.Enabled)
            {
                this._timer.Change(-1, -1);
                _task.ExecuteTask();
                if (this._task.Config.AllowLoop)
                {
                    this._timer.Change(this._Interval, this._Interval);
                }
                else
                {
                    Dispose();
                }
            }
            else
            {
                Dispose(); //若当前实例在线程中，应注销当前线程
            }
        }
        /// <summary>
        /// 实现Dispose.防止泄露_timer
        /// </summary>
        public void Dispose()
        {
            if (_timer != null)
            {
                lock (this)//TODO:?
                {
                    this._timer.Dispose();//
                    this._timer = null;//gc回收
                }
            }
        }

    }
}
