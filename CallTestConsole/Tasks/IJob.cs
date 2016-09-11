using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallTestConsole.Tasks
{

    public interface IJob
    {     
        /// <summary>
        /// 
        /// </summary>
        void Execute();
        /// <summary>
        /// 异常的处理;可供任务开发者自行实现
        /// </summary>
        void HandleException(Exception ex);
    }
}
