using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.PLC
{
    public interface IPLCConnection
    {
        void Connect();
        void Disconnect();
        bool IsConnected();
        bool IsClosed();
        bool Read(byte[] buffer, int offset, int count);
        bool Write(byte[] buffer, int offset, int count);
    }
}
