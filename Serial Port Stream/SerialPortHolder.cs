using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serial_Port_Stream
{
    internal class SerialPortHolder
    {
        public SerialPort SerialPort { get; private set; }
        public long NumberOfLineRead { get; private set; }
        public bool IsActive { get; private set; }

        public SerialPortHolder(SerialPort serialPort)
        {
            SerialPort = serialPort;
            NumberOfLineRead = 0;
            IsActive = false;
        }

        public void AddReadLines(string lines)
        {
            if (lines == null)
            {
                return;
            }

            string[] strings = lines.Split(Environment.NewLine);
            NumberOfLineRead += strings.Length;
        }

        public void ActivatePort(bool active)
        {
            IsActive = active;

            if (!active)
            {
                try
                {
                    SerialPort.Close();
                    //SerialPort.Dispose();
                }
                catch (IOException)
                {
                    // ignore, and just close
                }
            }
        }

        public static SerialPortHolder GetHolderFromSerialPort(SerialPort serialPort, List<SerialPortHolder> serialPorts)
        {
            foreach (var holder in serialPorts)
            {
                if (serialPort == holder.SerialPort)
                {
                    return holder;
                }
            }

            return null;
        }

        public static bool IsOnlyOneActive(List<SerialPortHolder> serialPorts)
        {
            return serialPorts.Count(x => x.IsActive) == 1;
        }

        public static bool IsNoneActive(List<SerialPortHolder> serialPorts)
        {
            return !serialPorts.Any(x => x.IsActive);
        }

        public static SerialPortHolder GetActiveSerialPortHolder(List<SerialPortHolder> serialPorts)
        {
            if (serialPorts.Count(x => x.IsActive) > 1)
            {
                throw new InvalidOperationException("there is more than one active auto port");
            }

            return serialPorts.First(x => x.IsActive);
        }
    }
}
