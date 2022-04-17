using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.QueueProcess
{
    public class MailElement
    {
        public string msg { get; set; }
    }
    public class MailQueue : QueueExecute<MailElement>
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public MailQueue(int numRepeat, int msInterval, bool continuous) : base(numRepeat, msInterval, continuous)
        {; }

        public override void DoCommand(QueueElement<MailElement> cmd)
        {
            _logger.Info(cmd.target.msg);
            Thread.Sleep(3 * 1000);
        }
    }
}
