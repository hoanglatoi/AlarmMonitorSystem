using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using Timer = System.Threading.Timer;

namespace AlarmMonitorSystem.Background
{
    public class SchedulerService
    {
        private List<ScheduleTimer2> timers = new List<ScheduleTimer2>();
        private static SchedulerService _instance = null!;
        private static object _lock = new object();

        private SchedulerService() 
        {

        }

        public static SchedulerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SchedulerService();
                        }
                    }             
                }
                return _instance;
            }
        }

        private void ScheduleTask(Action task, int sec=0, int min=0, int hour=0, int day=0, int month=0, SCHEDULE_TIMER_UNIT timerUnit = SCHEDULE_TIMER_UNIT.EVERYMIN)
        {
            DateTime now = DateTime.Now;
            DateTime firstRun;
            TimeSpan timeToGo;
            ScheduleTimer2 timer = null!;

            switch (timerUnit)
            {
                case SCHEDULE_TIMER_UNIT.EVERYMIN:
                    firstRun = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, sec, 0);
                    if (now > firstRun)
                    {
                        firstRun = firstRun.AddMinutes(1);
                    }

                    timeToGo = firstRun - now;
                    if (timeToGo <= TimeSpan.Zero)
                    {
                        timeToGo = TimeSpan.Zero;
                    }

                    timer = new ScheduleTimer2(task, 1, timeToGo, TIMER_UNIT.MINUTE);

                    timers.Add(timer);
                    break;
                case SCHEDULE_TIMER_UNIT.EVERYHOUR:
                    firstRun = new DateTime(now.Year, now.Month, now.Day, now.Hour, min, sec, 0);
                    if (now > firstRun)
                    {
                        firstRun = firstRun.AddHours(1);
                    }

                    timeToGo = firstRun - now;
                    if (timeToGo <= TimeSpan.Zero)
                    {
                        timeToGo = TimeSpan.Zero;
                    }

                    timer = new ScheduleTimer2(task, 1, timeToGo, TIMER_UNIT.HOUR);

                    timers.Add(timer);
                    break;
                case SCHEDULE_TIMER_UNIT.EVERYDAY:
                    firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, sec, 0);
                    if (now > firstRun)
                    {
                        firstRun = firstRun.AddDays(1);
                    }

                    timeToGo = firstRun - now;
                    if (timeToGo <= TimeSpan.Zero)
                    {
                        timeToGo = TimeSpan.Zero;
                    }

                    timer = new ScheduleTimer2(task, 1, timeToGo, TIMER_UNIT.DAY);

                    timers.Add(timer);
                    break;
            }
        }

        public static void EverySecondOfMin(int sec, Action task)
        {
            SchedulerService.Instance.ScheduleTask(task, sec, timerUnit: SCHEDULE_TIMER_UNIT.EVERYMIN);
        }

        public static void EveryMinOfHour(int min,int sec, Action task)
        {
            SchedulerService.Instance.ScheduleTask(task, sec, min, timerUnit: SCHEDULE_TIMER_UNIT.EVERYHOUR);
        }
        public static void EveryHourOfDay(int hour,int min, int sec, Action task)
        {
            SchedulerService.Instance.ScheduleTask(task, sec, min, hour, timerUnit: SCHEDULE_TIMER_UNIT.EVERYDAY);
        }
    }
}
