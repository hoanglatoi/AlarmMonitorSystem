using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.Notify;

namespace AlarmMonitorSystem.Models
{
    public enum AlarmState
    {
        Normal = 0,
        Occurrence,
        Reset,
        Confirm,
        ConfirmReset
    }
    public class Alarm : Subject
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ID { get; set; }

        public DateTime OccurDateTime { get; set; }
        public DateTime ConfirmDateTime { get; set; }
        public DateTime ResetDateTime { get; set; }

        private AlarmState state;
        public AlarmState GetState()
        {
            return this.state;
        }
        public void SetState(AlarmState value)
        {
            this.state = value;
            //StateChange();
        }

        public bool SendMailActive { get; set; }

        public bool PlayVoiceActive { get; set; }

        public Alarm()
        {
            state = AlarmState.Normal;
        }

        public void StateChange()
        {
            NotifyObservers(null);
        }

    }
}
