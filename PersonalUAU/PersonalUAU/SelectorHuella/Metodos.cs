using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using DPUruNet;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PersonalUAU.SelectorHuella
{
    class Metodos
    {
        //space for attributes
        private ReaderCollection objReaders;
        private List<DeviceInfo> objUsbs;
        private List<DeviceInfo> objDeviceReader;

        public void IndexDeviceList(System.Windows.Forms.ComboBox cboLista)
        {
            objReaders =  ReaderCollection.GetReaders();
            objDeviceReader = new List<DeviceInfo>();
            objUsbs = GetUSBDevices();

            Action work = delegate
            {
                foreach (DeviceInfo device in objUsbs)
                {
                    System.Console.WriteLine(device.deviceID);

                    foreach (Reader itemReader in objReaders)
                    {
                        if (device.deviceID.IndexOf(itemReader.Description.SerialNumber) != -1)
                        {
                            objDeviceReader.Add(device);
                            cboLista.Items.Add(device.deviceName);

                            return;
                        }
                    }
                }
            };

            work();
        }

        public Reader IndexDevice()
        {
            Reader objSelectedReader = null;
            ReaderCollection objReadersList = ReaderCollection.GetReaders();

            foreach (Reader itemReader in objReadersList)
            {
                objSelectedReader = itemReader;

                break;
            }

            return objSelectedReader;
        }

        public bool InitializeDevice(ref Reader objSelectedReader)
        {
            try
            {
                if (objSelectedReader == null)
                {
                    Console.WriteLine("No se encontraron dispositivos");
                    return false;
                }

                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;
                result = objSelectedReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    Console.WriteLine("No se pudo inicializar el dispositivo");

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Ha ocurrido un error");
                Console.WriteLine(Ex);

                return false;
            }
        }

        public Reader AssignDevice(System.Windows.Forms.ComboBox cboLista)
        {
            Reader objReader;

            if (cboLista.SelectedIndex >= 0)
            {
                objReader = objReaders[cboLista.SelectedIndex];

                return objReader;
            }
            else
            {
                return null;
            }
    
        }

        static public List<DeviceInfo> GetUSBDevices()
        {
            List<DeviceInfo> objDevices = new List<DeviceInfo>();

            ManagementObjectCollection objCollection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity where DeviceID Like ""USB%"""))
            objCollection = searcher.Get();

            var tem = objCollection.ToString();

            foreach (var device in objCollection)
            {
                var deviceInfo = new DeviceInfo();
                deviceInfo.deviceID = (string)device.GetPropertyValue("DeviceID");
                deviceInfo.pnpDeviceID = (string)device.GetPropertyValue("PNPDeviceID");
                deviceInfo.description = (string)device.GetPropertyValue("Description");
                deviceInfo.deviceName = (string)device.GetPropertyValue("Name");
                deviceInfo.caption = (string)device.GetPropertyValue("Caption");
                deviceInfo.service = (string)device.GetPropertyValue("Service");
                deviceInfo.manufacturer = (string)device.GetPropertyValue("Manufacturer");
                deviceInfo.status = (string)device.GetPropertyValue("Status");
                objDevices.Add(deviceInfo);
            }

            objCollection.Dispose();

            return objDevices;
        }

        public class DeviceInfo
        {
            public string deviceName { get; set; }
            public string deviceID { get; set; }
            public string pnpDeviceID { get; set; }
            public string description { get; set; }
            public string caption { get; set; }
            public string service { get; set; }
            public string manufacturer { get; set; }
            public string status { get; set; }
        }

    }
}
