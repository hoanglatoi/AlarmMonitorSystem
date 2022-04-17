using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.PLC
{
    public class PlcConnect 
    {
        private static PlcConnect instance = null!;
        private static object _lock = new object();
        public IPLCConnection? Plc = null!;

        private PlcConnect()
        {

        }
        public static PlcConnect Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new PlcConnect();
                    }
                }
                return instance;
            }
        }

        public void Init(PLC_TYPE plcType = PLC_TYPE.Mitsubishi)
        {
            this.Plc = PlcFactory.GetPlc(plcType);
        }

        public void Connect()
        {
            Plc?.Connect();
        }
        public void Disconnect()
        {
            Plc?.Disconnect();
        }

        public bool IsConnected()
        {
            if(Plc != null)
                return Plc.IsConnected();
            else return false;
        }

        public bool IsClosed()
        {
            if (Plc != null)
                return Plc.IsClosed();
            else return true;
        }

        public bool Read(byte[] buffer, int offset, int count)
        {
            if (Plc != null)
                return Plc.Read(buffer, offset, count);
            else return false;
        }

        public bool Write(byte[] buffer, int offset, int count)
        {
            if (Plc != null)
                return Plc.Write(buffer, offset, count);
            else return false;
        }
    }
}
