using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.PLC
{
    public class MitsubishiPLC : IPLCConnection
    {
        public void Connect()
        {

        }
        public void Disconnect()
        {

        }

        public bool IsConnected()
        {
            return IsConnected();
        }

        public bool IsClosed()
        {
            return IsClosed();
        }

        public bool Read(byte[] buffer, int offset, int count)
        {
            return Read(buffer, offset, count);
        }

        public bool Write(byte[] buffer, int offset, int count)
        {
            return Read(buffer, offset, count);
        }
    }
}
