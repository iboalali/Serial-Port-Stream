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
                    serialPort.Close();
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form is closing with following reason: " + e.CloseReason);

            if (isOpen)
            {
                serialPort.Close();
            }

            serialPort?.Dispose();
        }
    }
}
