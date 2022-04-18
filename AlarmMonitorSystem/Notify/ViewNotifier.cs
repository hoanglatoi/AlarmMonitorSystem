using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.Data;
using AlarmMonitorSystem.Models;
using AlarmMonitorSystem.QueueProcess;
using AlarmMonitorSystem.SubForm;

namespace AlarmMonitorSystem.Notify
{
    internal class ViewNotifier : Observer
    {
        private static readonly log4net.ILog _logger
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        public ViewNotifier(Subject subject)
        {
            Subject = subject;
            Subject.AttachObserver(this);
        }

        public override void Notify(object? arg)
        {
            if (Subject is Alarm)
            {
                Alarm alarm = (Alarm)Subject;
                // Update View
                if(alarm.GetState() == AlarmState.Occurrence)
                    RouteFormManager.Instance.SumaryFrm.ChangeView(alarm.ID);
            }
        }
    }
}
