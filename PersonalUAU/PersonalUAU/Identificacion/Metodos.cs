using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PersonalUAU.Identificacion
{
    public class Metodos
    {
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;
        private Thread identifyThreadHandle;
        private bool reset = false;
        Reader objReader;
        PersonalUAU.DigitalPersona objReaderMethods;
        PersonalUAU.SelectorHuella.Metodos objDeviceReader;
        //PersonalUAU.CapturaHuellaWPF.Metodos objCaptureWPF;
        List<Clases.Persona> listPersons;
        VistaIdentificacion menuPrincipal;

        public Metodos(Reader objReader,List<Clases.Persona> listPersons,VistaIdentificacion menuPrincipal)
        {
            this.objReader = objReader;
            this.listPersons = listPersons;
            this.menuPrincipal = menuPrincipal;
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            objReaderMethods = new PersonalUAU.DigitalPersona();
            objDeviceReader = new SelectorHuella.Metodos();
            //objCaptureWPF = new PersonalUAU.CapturaHuellaWPF.Metodos(objReader,menuPrincipal.picHuella);
            //objCaptureWPF.StartCaptures();
        }

        public void StartIdentify()
        {
            //identifyThreadHandle = new Thread(() => IdentifyThread(buscarPersona));
            identifyThreadHandle = new Thread(IdentifyThread);
            identifyThreadHandle.IsBackground = true;
            identifyThreadHandle.Start();
        }

        private void IdentifyThread()
        {

            while (!reset)
            {
                Fid fid = null;

                if (!CaptureFinger(ref fid))
                {
                    //break;
                }

                if (objReader == null)
                {
                    objReader = objDeviceReader.IndexDevice();
                    objDeviceReader.InitializeDevice(ref objReader);
                }

                if (fid == null)
                {
                    continue;
                }

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(fid, Constants.Formats.Fmd.ANSI);

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    //break;

                    if (objReader != null)
                    {
                        objReader.Dispose();
                        objReader = null;
                    }
                    return;
                }
                    
                int thresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;

                Fmd aux = resultConversion.Data;
                Fmd temp;

                foreach (Clases.Persona item in listPersons)
                {
                    temp = Fmd.DeserializeXml(item.huella);

                    CompareResult identifyResult = Comparison.Compare(aux, 0, temp, 0);

                    if (identifyResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        break;
                    }

                    if (identifyResult.Score < thresholdScore)
                    {
                        SendMessage("Identificado");
                        MessageBox.Show("Identificado");
                        Thread.Sleep(3000);
                        SendMessage("Buscando...");
                        break;
                    }
                }
            }

            if (objReader != null)
                objReader.Dispose();
        }

        public bool CaptureFinger(ref Fid fid)
        {
            try
            {
                Constants.ResultCode result = objReader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    //MessageBox.Show("Get Status Error:  " + result);
                    if (objReader != null)
                    {
                        objReader.Dispose();
                        objReader = null;
                    }
                    return false;
                }

                if ((objReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                    return true;
                }
                else if ((objReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    objReader.Calibrate();
                }
                else if ((objReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    //MessageBox.Show("Get Status:  " + Lector.Status.Status);

                    if (objReader != null)
                    {
                        objReader.Dispose();
                        objReader = null;
                    }
                    return false;
                }

                CaptureResult captureResult = objReader.Capture(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 5000, objReader.Capabilities.Resolutions[0]);

                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    //MessageBox.Show("Error:  " + captureResult.ResultCode);

                    if (objReader != null)
                    {
                        objReader.Dispose();
                        objReader = null;
                    }
                    return false;
                }

                if (captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_CANCELED)
                {
                    return false;
                }

                if ((captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_NO_FINGER || captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_TIMED_OUT))
                {
                    return true;
                }

                if ((captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_FAKE_FINGER))
                {
                    //MessageBox.Show("Quality Error:  " + captureResult.Quality);

                    return true;
                }

                fid = captureResult.Data;

                return true;
            }
            catch
            {
                //MessageBox.Show("An error has occurred.");
                if (objReader != null)
                {
                    objReader.Dispose();
                    objReader = null;
                }
                return false;
            }
        }

        private delegate void SendMessageCallback(string payload);

        private void SendMessage(string payload)
        {
            menuPrincipal.Dispatcher.BeginInvoke(new Action(delegate()
            {
                menuPrincipal.lblMensaje.Content = payload;
            }));

        }

        public void StopIdentify()
        {
            if (objReader != null)
            {
                reset = true;
                objReader.CancelCapture();

                if (identifyThreadHandle != null)
                {
                    identifyThreadHandle.Join(5000);
                }
            }
            else
            {
                reset = true;
                if (identifyThreadHandle != null)
                {
                    identifyThreadHandle.Join(5000);
                }
            }
        }
    }
}
