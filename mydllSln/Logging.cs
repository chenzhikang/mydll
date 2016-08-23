using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;
namespace MyDllCollection
{
    public class Logging
    {

        public Logging()
        {

        }
        public static void LogInfo(Type t, string msg)
        {
           //  BasicConfigurator.Configure();//配置简单的仓库 logger 和appender。
            //   XmlConfigurator.ConfigureAndWatch(new FileInfo(@"D:\Repos\MyDllCollection\CallTestConsole\app.config"));
            var loggers = LogManager.GetCurrentLoggers();
            var mylogger = LogManager.GetLogger("mylogger");
            mylogger.Debug("asdfsadfasdf");
            var logger = LogManager.GetLogger(t);
            logger.Debug(msg);
            logger.Info(msg);
            logger.Error(msg);
        }
    }
}
