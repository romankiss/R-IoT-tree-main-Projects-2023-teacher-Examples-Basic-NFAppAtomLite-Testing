using Iot.Device.Button;
using Iot.Device.Mfrc522;
using Iot.Device.Rfid;
using Iot.Device.Ws28xx.Esp32;
using nanoFramework.AtomLite;
using nanoFramework.Hardware.Esp32;
using nanoFramework.Logging;
using nanoFramework.Runtime.Native;
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using Iot.Device.Ssd13xx;
using System.Drawing;
using System.Text;


// https://docs.nanoframework.net/devicesdetails/Mfrc522/README.html
// https://github.com/nanoframework/nanoFramework.IoT.Device/blob/develop/devices/Mfrc522/samples/Program.cs

namespace NFAppAtomLite_Testing
{
    public class Program
    {
        static Sk6812 neo = null;
        static int ii = 0;

        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            GpioButton button = new(39, debounceTime: TimeSpan.FromMilliseconds(200));
            button.Press += Button_Press;
            neo = AtomLite.NeoPixel;
            //neo = new Sk6812(22, 1);
            neo.Image.SetPixel(0, 0, 0, 0, 10);
            neo.Update();
    
            Thread.Sleep(Timeout.Infinite);
        }

        private static void Button_Press(object sender, EventArgs e)
        {
            int i = ii++ & 0x3;
            Debug.WriteLine($"Button has been pressed, rgb = {i}");
            neo.Image.SetPixel(0, 0, (byte)(i==1?10:0), (byte)(i == 2 ? 10 : 0), (byte)(i == 3 ? 10 : 0));
            neo.Update();
        }
    }
}
//neo.Image.SetPixel(0, 0, 0, (byte)(new Random()).Next(10), 0);
//neo.Update();