using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Background
{
    public class ScheduleTimer : TimedHostedService
    {
        private List<Schedule> schedules = new List<Schedule>();
        private static ScheduleTimer _instance = null!;
        private static object _lock = new object();
        private ScheduleTimer() :base(100,0,TIMER_UNIT.MILISECOND) //100 milisecond
        {
            //base.StartAsync(new CancellationToken());
        }

        public static ScheduleTimer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ScheduleTimer();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddSchedule(Schedule schedule)
        {
            schedules.Add(schedule);
        }

        public override void DoWork(object? state)
        {
            //Get time now without miliseconds
            DateTime now = Convert.ToDateTime( DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            //run time determine, runtime without miliseconds
            DateTime runTime = DateTime.MaxValue;         
            foreach (Schedule schedule in schedules)
            {         
                switch (schedule.SCHEDULE_TIMER_UNIT)
                {
                    case SCHEDULE_TIMER_UNIT.EVERYMIN:                       
                        runTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, schedule.Second, 0);                       
                        break;
                    case SCHEDULE_TIMER_UNIT.EVERYHOUR:
                        runTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, schedule.Min, schedule.Second, 0);
                        break;
                    case SCHEDULE_TIMER_UNIT.EVERYDAY:
                        runTime = new DateTime(now.Year, now.Month, now.Day, schedule.Hour, schedule.Min, schedule.Second, 0);
                        break;
                    case SCHEDULE_TIMER_UNIT.EVERYWEEK:
                        runTime = new DateTime(now.Year, now.Month, now.Day, schedule.Hour, schedule.Min, schedule.Second, 0);
                        break;
                    case SCHEDULE_TIMER_UNIT.EVERYMONTH:
                        runTime = new DateTime(now.Year, now.Month, schedule.Day, schedule.Hour, schedule.Min, schedule.Second, 0);
                        break;
                    case SCHEDULE_TIMER_UNIT.EVERYYEAR:
                        runTime = new DateTime(now.Year, schedule.Month, schedule.Day, schedule.Hour, schedule.Min, schedule.Second, 0);
                        break;
                }
                if (now == runTime && schedule.SCHEDULE_TIMER_UNIT != SCHEDULE_TIMER_UNIT.EVERYWEEK)
                {
                    if(schedule.Run == true) // prevent double
                    {
                        Task task = new Task(schedule.Action!);
                        task.Start();
                        schedule.Run = false;
                    }                  
                }
                else if (now == runTime && schedule.SCHEDULE_TIMER_UNIT == SCHEDULE_TIMER_UNIT.EVERYWEEK)
                {
                    if(now.DayOfWeek == schedule.DayOfWeek)
                    {
                        if (schedule.Run == true) // prevent double
                        {
                            Task task = new Task(schedule.Action!);
                            task.Start();
                            schedule.Run = false;
                        }
                    }
                }else if(now != runTime) 
                {
                    schedule.Run = true;
                }
            }
        }
    }

    public class Schedule
    {
        public bool Run { get; set; }
        public Action? Action { get; set; }
        public SCHEDULE_TIMER_UNIT SCHEDULE_TIMER_UNIT { get; set; }

        public int Second { get; set; }
        public int Min { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DayOfWeek DayOfWeek { get; set; }//0~6

        public Schedule(Action? action, SCHEDULE_TIMER_UNIT mode, int sec=0, int min=0, int hour=0, int day=1, DayOfWeek dayOfWeek =DayOfWeek.Monday, int month=1, int year=2000)
        {
            this.Action = action;
            this.Second = sec;
            this.Min = min;
            this.Hour = hour;
            this.Day = day;
            this.DayOfWeek = dayOfWeek;
            this.Month = month;
            this.Year = year;
            this.SCHEDULE_TIMER_UNIT = mode;
            this.Run = false;
        }
    }

    // Builer class
    public interface IScheduleBuilder
    {
        IScheduleBuilder SetAction(Action action);
        IScheduleBuilder SetScheduleMode(SCHEDULE_TIMER_UNIT mode);
        IScheduleBuilder SetSecond(int sec);
        IScheduleBuilder SetMin(int min);
        IScheduleBuilder SetHour(int hour);
        IScheduleBuilder SetDay(int day);
        IScheduleBuilder SetMonth(int month);
        IScheduleBuilder SetYear(int year);
        IScheduleBuilder SetDayOfWeek(DayOfWeek dayOfWeek);
        Schedule Build();
    }

    public class ScheduleBuilder : IScheduleBuilder
    {
        private bool run = false;
        private int sec = 0;
        private int min = 0;
        private int hour = 0;
        private int day = 1;
        private int month = 1;
        private int year = 2000;
        private DayOfWeek dayOfWeek = DayOfWeek.Monday;
        private Action? action = null;
        private SCHEDULE_TIMER_UNIT mode = SCHEDULE_TIMER_UNIT.EVERYMIN;

        public IScheduleBuilder SetAction(Action action)
        {
            this.action = action;
            return this;
        }

        public IScheduleBuilder SetScheduleMode(SCHEDULE_TIMER_UNIT mode)
        {
            this.mode = mode;
            return this;
        }
        public IScheduleBuilder SetSecond(int sec)
        {
            this.sec = sec;
            return this;
        }
        public IScheduleBuilder SetMin(int min)
        {
            this.min = min;
            return this;
        }
        public IScheduleBuilder SetHour(int hour)
        {
            this.hour = hour;
            return this;
        }
        public IScheduleBuilder SetDay(int day)
        {
            this.day = day;
            return this;
        }
        public IScheduleBuilder SetMonth(int month)
        {
            this.month = month;
            return this;
        }
        public IScheduleBuilder SetYear(int year)
        {
            this.year = year;
            return this;
        }
        public IScheduleBuilder SetDayOfWeek(DayOfWeek dayOfWeek)
        {
            this.dayOfWeek = dayOfWeek;
            return this;
        }

        public Schedule Build()
        {
            return new Schedule(
                action:this.action, 
                mode:this.mode, 
                sec:this.sec, 
                min:this.min, 
                hour:this.hour, 
                day:this.day, 
                month:this.month, 
                year:this.year, 
                dayOfWeek:this.dayOfWeek);
        }

    }
}
