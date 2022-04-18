using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.Background;
using AlarmMonitorSystem.Data;
using AlarmMonitorSystem.QueueProcess;

namespace AlarmMonitorSystem
{
    public class Startup : IDisposable
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        public Startup()
        {
            CommonResource.Instance.MailQueue.QueueClear();
            CommonResource.Instance.SoundQueue.QueueClear();
            CommonResource.Instance.MailQueue.Start();
            CommonResource.Instance.SoundQueue.Start();

            PlcCom plcCom = new PlcCom(interval:5, delay:0, timerUnit: TIMER_UNIT.SECOND);
            plcCom.StartAsync(new CancellationToken());
        }

        public void Dispose()
        {
            CommonResource.Instance.MailQueue.Stop();
            CommonResource.Instance.SoundQueue.Stop();
        }

        public void TestScheduleTimer()
        {
            var secondOfMin_Action = new Action(SecondOfMin);
            var minOfHour_Action = new Action(MinOfHour);
            var hourOfday_Action = new Action(HourOfDay);
            var dayOfWeek_Action = new Action(DayOfWeek);
            var dayOfMonth_Action = new Action(DayOfMonth);
            var monthOfYear_Action = new Action(MonthOfYear);

            //Schedule secondOfMin_Schedule = new Schedule(secondOfMin_Action, SCHEDULE_TIMER_UNIT.EVERYMIN, sec:0, min: 20, hour: 12, day: 17, month: 4);
            //ScheduleTimer.Instance.AddSchedule(secondOfMin_Schedule);
            Schedule secondOfMin_Schedule = new ScheduleBuilder()
                .SetAction(secondOfMin_Action)
                .SetScheduleMode(SCHEDULE_TIMER_UNIT.EVERYMIN)
                .SetSecond(0)
                .SetMin(0)
                .SetHour(13)
                .SetDay(17)
                .SetMonth(4)
                .SetDayOfWeek(System.DayOfWeek.Sunday)
                .SetYear(2022)
                .Build();
            ScheduleTimer.Instance.AddSchedule(secondOfMin_Schedule);

            Schedule minOfHour_Schedule = new Schedule(minOfHour_Action, SCHEDULE_TIMER_UNIT.EVERYHOUR, sec: 0, min: 20, hour: 12, day: 17, month: 4);
            ScheduleTimer.Instance.AddSchedule(minOfHour_Schedule);

            Schedule hourOfday_Schedule = new Schedule(hourOfday_Action, SCHEDULE_TIMER_UNIT.EVERYDAY, sec: 0, min: 20, hour: 12, day: 17, month: 4);
            ScheduleTimer.Instance.AddSchedule(hourOfday_Schedule);

            //Schedule dayOfWeek_Schedule = new Schedule(dayOfWeek_Action, SCHEDULE_TIMER_UNIT.EVERYWEEK, sec: 0, min: 23, hour: 12, day: 17, month: 4, dayOfWeek:System.DayOfWeek.Sunday);
            //ScheduleTimer.Instance.AddSchedule(dayOfWeek_Schedule);
            Schedule dayOfWeek_Schedule = new ScheduleBuilder()
                .SetAction(dayOfWeek_Action)
                .SetScheduleMode(SCHEDULE_TIMER_UNIT.EVERYWEEK)
                .SetSecond(30)
                .SetMin(4)
                .SetHour(13)
                .SetDay(17)
                .SetMonth(4)
                .SetDayOfWeek(System.DayOfWeek.Sunday)
                .SetYear(2022)
                .Build();
            ScheduleTimer.Instance.AddSchedule(dayOfWeek_Schedule);

            Schedule dayOfMonth_Schedule = new Schedule(dayOfMonth_Action, SCHEDULE_TIMER_UNIT.EVERYMONTH, sec: 0, min: 20, hour: 12, day: 17, month: 4);
            ScheduleTimer.Instance.AddSchedule(dayOfMonth_Schedule);

            Schedule monthOfYear_Schedule = new Schedule(monthOfYear_Action, SCHEDULE_TIMER_UNIT.EVERYMONTH, sec: 0, min:20, hour:12, day:17,month:4);
            ScheduleTimer.Instance.AddSchedule(monthOfYear_Schedule);

            ScheduleTimer.Instance.StartAsync(new CancellationToken());
        }

        public void SecondOfMin()
        {
            Console.WriteLine("second of min");
            _logger.Info("second of min");
        }

        public void MinOfHour()
        {
            _logger.Info("MinOfHour");
        }

        public void HourOfDay()
        {
            _logger.Info("HourOfDay");
        }

        public void DayOfWeek()
        {
            _logger.Info("DayOfWeek");
        }

        public void DayOfMonth()
        {
            _logger.Info("DayOfMonth");
        }

        public void MonthOfYear()
        {
            _logger.Info("MonthOfYear");
        }
    }
}
