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
        private bool Isrunning { get; set; }

        /// <summary>
        /// 配置项
        /// </summary>
        public JobConfiguration Config { get; }
        private IJob _job;

        public Task(IJob job, JobConfiguration config)
        {
            _job = job;
            Config = config;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void ExecuteTask()
        {
            if (Isrunning)
            {
                return;
            }
            Isrunning = true;
            #region MyRegion
            if (_job == null && !string.IsNullOrEmpty(this.Config.TypeName))//如果未传入的job对象，且TypeName不为空，就反射出一个类实例
            {
                _job = this.CreateObj(this.Config.TypeName);
            }
            #endregion            

            try
            {
                _job.Execute();
            }
            catch (Exception ex)
            {
                _job.HandleException(ex);
                if (Config.StopOnException)
                {
                    this.Config.Enabled = false;
                }
            }
            Isrunning = false;
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
