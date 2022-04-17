using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmMonitorSystem.QueueProcess;

namespace AlarmMonitorSystem.Data
{
    public class CommonResource
    {
        private static CommonResource instance = null!;
        private static object _lock = new object();

        private MailQueue? mailQueue;
        private SoundQueue? soundQueue;

        private CommonResource()
        {
            MailQueue = new MailQueue(1, 30 * 1000, false);
            SoundQueue = new SoundQueue(1, 1000, true);
        }
        public static CommonResource Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new CommonResource();                        
                        }
                    }
                }
                return instance;
            }
        }

        public MailQueue MailQueue { get => mailQueue; set => mailQueue = value; }
        public SoundQueue SoundQueue { get => soundQueue; set => soundQueue = value; }
    }
}
