using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.Data;
using AlarmMonitorSystem.Models;
using AlarmMonitorSystem.QueueProcess;

namespace AlarmMonitorSystem.Notify
{
    public class EmailNotifier : Observer
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        public EmailNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify(object? arg)
        {
            if(Subject is Alarm)
            {
                Alarm alarm = (Alarm)Subject;
                if (alarm.SendMailActive)
                {
                    // enqueue  
                    // send something
                    _logger.Info("alarm number: " + alarm.ID);
                    _logger.Info("send mail enqueue: " + alarm.GetState());

                    MailElement me = new MailElement() { msg = "alarm number: " + alarm.ID + " ," + "send mail: " + alarm.GetState() };
                    CommonResource.Instance.MailQueue.QueueAdd(me);
                }
            }
        }
    }
}
