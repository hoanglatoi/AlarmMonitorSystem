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
    public class VoiceNotifier : Observer
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        public VoiceNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify(object? arg)
        {
            if (Subject is Alarm)
            {
                Alarm alarm = (Alarm)Subject;
                if (alarm.SendMailActive)
                {
                    // enqueue 
                    // play something
                    _logger.Info("alarm number: " + alarm.ID);
                    _logger.Info("play voice enqueue: " + alarm.GetState());

                    SoundElement me = new SoundElement() { msg = "alarm number: " + alarm.ID + " ," + "play voice: " + alarm.GetState() };
                    CommonResource.Instance.SoundQueue.QueueAdd(me);
                }
            }
        }
    }
}
