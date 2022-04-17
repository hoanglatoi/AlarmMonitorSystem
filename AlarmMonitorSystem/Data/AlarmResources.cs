using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.Models;
using AlarmMonitorSystem.Notify;

namespace AlarmMonitorSystem
{
    public class AlarmResources
    {
        private static AlarmResources instance = null!;
        private static object _lock = new object();
        private List<Alarm> alarmList = new List<Alarm>();

        private AlarmResources()
        {

        }
        public static AlarmResources Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new AlarmResources();
                            instance.InitAlarmList();
                        }                   
                    }
                }
                return instance;
            }
        }

        public List<Alarm> GetAlarmList()
        {
            lock(_lock)
            {
                return alarmList;
            }
        }

        public void InitAlarmList()
        {
            lock (_lock)
            {
                // Read seeting from setting file

                // Update name, settings for Alarm
                for (int i = 0; i < 10; i++)
                {
                    Alarm alarm = new Alarm();
                    _ = new EmailNotifier(alarm);
                    _ = new VoiceNotifier(alarm);
                    _ = new ViewNotifier(alarm);
                    _ = new UpdateHistories(alarm);

                    alarm.ID = i;
                    alarm.Name = "Alarm" + i.ToString();
                    alarm.Description = "";
                    alarm.SendMailActive = true;
                    alarm.PlayVoiceActive = true;
                    //alarm.SetState(AlarmState.Normal);

                    alarmList.Add(alarm);
                }
            }
        }

        public void UpdateAllSettingAlarm()
        {
            lock(_lock)
            {
                // Read seeting from setting file

                // Update name, settings for Alarm
                for(int i = 0; i < alarmList.Count; i++)
                {
                    // update some thing
                }
            }          
        }

        public void UpdateSettingAlarm(int alarmID, string? name=null, string? des=null, bool? sendMailActive = null, bool? playVoiceActive= null)
        {
            lock (_lock)
            {
                if(name != null)
                    alarmList[alarmID].Name = name;
                if(des != null)
                    alarmList[alarmID].Description = des;
                if (sendMailActive != null)
                    alarmList[alarmID].SendMailActive = sendMailActive ?? false;
                if (playVoiceActive != null)
                    alarmList[alarmID].PlayVoiceActive = playVoiceActive ?? false;
            }
        }

        public void UpdateStateAlarm(int alarmID, AlarmState alarmState)
        {
            lock(_lock)
            {
                alarmList[alarmID].SetState(alarmState);
                alarmList[alarmID].StateChange();
            }
        }
    }
}
