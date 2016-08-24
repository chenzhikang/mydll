using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{
    public class TaskThread
    {
        Timer _timer;
        List<Task> _tasks = new List<Task>();
        public TaskThread()
        {

        }
        public void InitStartTimer()
        {
            if (this._timer != null)
            {
                this._timer = new Timer(this.TimerHandler);
            }
        }
        private void TimerHandler(object state)
        {
            this._timer.Change(-1, -1);
            //执行任务的具体代码 
            foreach (Task t in _tasks)
            {
                t.ExecuteTask();
            }
            //todo:单次任务
        }
        public void Run()
        {

        }
        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

    }
}
