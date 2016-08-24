using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace CallTestConsole.Tasks
{

    /// <summary>
    /// 任务本身
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 配置项
        /// </summary>
        private JobConfiguration Config { get; set; }
        private IJob _job;
        public bool IsRunning { get; private set; }
        public Task(IJob job)
        {
            _job = job;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void ExecuteTask()
        {
            this.IsRunning = true;           
            _job = this.CreateObj(this.Config.TypeName);
            try
            {
                _job.Execute();
            }
            catch (Exception ex)
            {
                _job.HandleException(ex);
                //TODO：log
            }
            this.IsRunning = false;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <returns></returns>
        private IJob CreateObj(string TypeName)
        {
            IJob job = null;
            if (this.Config.Enabled)
            {
                try
                {
                    //反射生成对象
                    Type type = Type.GetType(TypeName);
                    if (type != null)
                    {
                        job = (IJob)type.Assembly.CreateInstance(TypeName);
                    }
                }
                catch (Exception)
                {
                    //log
                }

            }
            return job;
        }


        //TODO:立即执行功能
    }
}
