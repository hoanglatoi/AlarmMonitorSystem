using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.PLC
{
    public enum PLC_TYPE
    {
        Mitsubishi,
        Simen
    }
    public class PlcFactory
    {
        private PlcFactory()
        {

        }
        public static IPLCConnection? GetPlc(PLC_TYPE plcType)
        {
            switch (plcType)
            {
                case PLC_TYPE.Mitsubishi:
                    return new MitsubishiPLC();
                case PLC_TYPE.Simen:
                    return new SimenPlc();
                default: 
                    return null;
            }
        }
    }
}
