using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Background
{
    public enum TIMER_UNIT
    {
        MILISECOND,
        SECOND,
        MINUTE,
        HOUR,
        DAY,
        WEEK,
        MONTH,
        YEAR
    }

    public enum SCHEDULE_TIMER_UNIT
    {
        EVERYMIN,
        EVERYHOUR,
        EVERYDAY,
        EVERYWEEK,
        EVERYMONTH,
        EVERYYEAR
    }
}
