using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Notify
{
    public abstract class Observer
    {
        protected Subject? Subject;
        public abstract void Notify(object? arg);
    }
}
