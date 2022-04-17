using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.PLC;

namespace AlarmMonitorSystem.Background
{
    internal class PlcCom : TimedHostedService
    {
        public PlcCom(int interval, int delay, TIMER_UNIT timerUnit = TIMER_UNIT.MILISECOND) : base(interval, delay, timerUnit)
        {
        }

        public override void DoWork(object? state)
        {
            byte[] data = null;
            PlcConnect.Instance.Read(data, 32, 32);

            // Check if plc alarm state is deffrent with current alarm in alarm list, update state of alarm
            //Example
            AlarmResources.Instance.UpdateStateAlarm(alarmID: 1, alarmState: Models.AlarmState.Occurrence);
            AlarmResources.Instance.UpdateStateAlarm(alarmID: 5, alarmState: Models.AlarmState.Reset);
        }
    }
}
