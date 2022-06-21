using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serial_Port_Stream
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private readonly Parity parity = Parity.None;
        private readonly int baudRate = 9600;
        private readonly int dataBits = 8;
        private readonly StopBits stopBits = StopBits.One;
        private bool isOpen = false;

        private readonly List<SerialPortHolder> autoPortsHolder = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void SerialPort_Disposed(object sender, EventArgs e)
        {
            Debug.WriteLine("Serial port disposed");
        }

        private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            Debug.WriteLine("Serial port PIN changed: " + e.EventType);
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debug.WriteLine("Serial port error received: " + e.EventType);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();

            if (inData == null)
            {
                return;
            }

            bool alreadyNewLine = inData.EndsWith(Environment.NewLine);

            textBoxContent.Invoke((MethodInvoker)delegate
            {
                textBoxContent.AppendText(inData + (!alreadyNewLine ? Environment.NewLine : ""));
            });
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Serial port connecting");
            if (!isOpen)
            {
                serialPort = new SerialPort("COM" + textBoxPort.Text, baudRate, parity, dataBits, stopBits)
                {
                    NewLine = Environment.NewLine
                };

                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.ErrorReceived += SerialPort_ErrorReceived;
                serialPort.PinChanged += SerialPort_PinChanged;
                serialPort.Disposed += SerialPort_Disposed;

                try
                {
                    serialPort.Open();
                    serialPort.ReadExisting();
                    isOpen = true;
                }
                catch (UnauthorizedAccessException)
                {
                    // ignore exception
                    System.Media.SystemSounds.Asterisk.Play();
                }
                catch (FileNotFoundException)
                {
                    // ignore exception
                    System.Media.SystemSounds.Asterisk.Play();
                }
                catch (ArgumentException)
                {
                    // ignore exception
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
            else
            {
                Debug.WriteLine("Serial port is already connected");
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Serial port disconnecting");
            if (isOpen)
            {
                try
                {
                    serialPort?.Close();
                    isOpen = false;
                }
                catch (IOException)
                {
                    // ignore, and just close
                }
            }
            else
            {
                Debug.WriteLine("Serial port is already disconnected");
                System.Media.SystemSounds.Asterisk.Play();
            }

            foreach (var holder in autoPortsHolder)
            {
                holder.ActivatePort(false);
            }

            buttonAutoOpen.Text = "Open All Ports";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form is closing with following reason: " + e.CloseReason);

            if (isOpen)
            {
                try
                {
                    serialPort?.Close();
                    isOpen = false;
                }
                catch (IOException)
                {
                    // ignore, and just close
                }
            }

            serialPort?.Dispose();

            foreach (var holder in autoPortsHolder)
            {
                if (holder.SerialPort.IsOpen)
                {
                    holder.SerialPort.Close();
                }

                holder.SerialPort.Dispose();
            }
        }

        private long autoPortDetectingStartTime;
        private const long waitUntilAutoClose = 10000;

        private void buttonAutoOpen_Click(object sender, EventArgs e)
        {
            if (buttonAutoOpen.Text == "Close All Ports")
            {
                foreach (var holder in autoPortsHolder)
                {
                    holder.ActivatePort(false);
                }

                buttonAutoOpen.Text = "Open All Ports";
                return;
            }

            autoPortDetectingStartTime = DateTime.Now.Ticks;
            var ports = SerialPort.GetPortNames();

            var button = (Button)sender;
            button.Text = "Close All Ports";

            foreach (var port in ports)
            {
                var serialPort = new SerialPort(port, baudRate, parity, dataBits, stopBits)
                {
                    NewLine = Environment.NewLine
                };

                serialPort.DataReceived += AutoSerialPort_DataReceived; ;
                serialPort.ErrorReceived += AutoSerialPort_ErrorReceived;
                serialPort.PinChanged += AutoSerialPort_PinChanged;
                serialPort.Disposed += AutoSerialPort_Disposed;

                try
                {
                    serialPort.Open();
                    serialPort.ReadExisting();
                }
                catch (UnauthorizedAccessException)
                {
                    // ignore exception
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                }
                catch (FileNotFoundException)
                {
                    // ignore exception
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                }

                if (serialPort.IsOpen)
                {
                    writeToScreen(serialPort, "opened");
                }

                autoPortsHolder.Add(new SerialPortHolder(serialPort));
            }

            //if (SerialPortHolder.IsNoneActive(autoPortsHolder))
            //{
            //    buttonAutoOpen.Text = "Auto Open Port";
            //}
        }

        private void AutoSerialPort_Disposed(object sender, EventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            writeToScreen(sp, "disposed");

        }

        private void AutoSerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            /*
             CtsChanged = 8,
             DsrChanged = 16,
             CDChanged = 32,
             Break = 64,
             Ring = 256,
            */
            SerialPort sp = (SerialPort)sender;
            string content = e.EventType.ToString();
            writeToScreen(sp, content);
        }

        private void AutoSerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            /*
             RXOver = 1,
             Overrun = 2,
             RXParity = 4,
             Frame = 8,
             TXFull = 256,
             */
            SerialPort sp = (SerialPort)sender;
            string content = e.EventType.ToString();
            writeToScreen(sp, content);

        }

        private void AutoSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (SerialPortHolder.IsOnlyOneActive(autoPortsHolder))
            //{
            //    SerialPort sp = SerialPortHolder.GetActiveSerialPortHolder(autoPortsHolder).SerialPort;
            //    string inData = sp.ReadExisting();
            //
            //    if (inData == null)
            //    {
            //        return;
            //    }
            //
            //    bool alreadyNewLine = inData.EndsWith(Environment.NewLine);
            //
            //    textBoxContent.Invoke((MethodInvoker)delegate
            //    {
            //        textBoxContent.AppendText(inData + (!alreadyNewLine ? Environment.NewLine : ""));
            //    });
            //}
            //else
            //{
            //    SerialPort sp = (SerialPort)sender;
            //    string inData = sp.ReadExisting();
            //
            //    if (inData == null)
            //    {
            //        return;
            //    }
            //
            //    var serialPortHolder = SerialPortHolder.GetHolderFromSerialPort(sp, autoPortsHolder);
            //    serialPortHolder.AddReadLines(inData);
            //
            //    if (DateTime.Now.Ticks - autoPortDetectingStartTime > waitUntilAutoClose)
            //    {
            //        serialPortHolder.ActivatePort(serialPortHolder.NumberOfLineRead >= 100);
            //    }
            //}
            //
            //if (DateTime.Now.Ticks - autoPortDetectingStartTime > waitUntilAutoClose)
            //{
            //    if (SerialPortHolder.IsNoneActive(autoPortsHolder))
            //    {
            //        buttonAutoOpen.Text = "Auto Open Port";
            //    }
            //}

            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();

            if (inData == null)
            {
                return;
            }

            writeToScreen(sp, inData);
        }

        private void writeToScreen(SerialPort serialPort, String content)
        {
            string portName = serialPort.PortName;
            string[] lines = content.Split(Environment.NewLine, StringSplitOptions.None);

            if (lines.Length == 0)
            {
                textBoxContent.Invoke((MethodInvoker)delegate
                {
                    textBoxContent.AppendText(portName + (portName.Length == 4 ? " " : "") + ": " + Environment.NewLine);
                });
            }
            else if (lines.Length == 1)
            {
                string line = portName + (portName.Length == 4 ? " " : "") + ": " + lines[0];
                textBoxContent.Invoke((MethodInvoker)delegate
                {
                    textBoxContent.AppendText(line + Environment.NewLine);
                });
            }
            else
            {
                string firstLine = portName + ": " + lines[0];
                for (int i = 1; i < lines.Length; i++)
                {
                    lines[i] = "     : " + lines[i] + Environment.NewLine;
                }

                textBoxContent.Invoke((MethodInvoker)delegate
                {
                    foreach (var line in lines)
                    {
                        textBoxContent.AppendText(line);
                    }
                });
            }
        }

        private void buttonClearScreen_Click(object sender, EventArgs e)
        {
            textBoxContent.Text = String.Empty;
        }
    }
}
