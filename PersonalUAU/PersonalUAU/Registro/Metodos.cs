using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Threading;

namespace PersonalUAU.Registro
{
    class Metodos
    {
        //space for attributes
        private SelectorHuella.Metodos objDevice;
        private Thread objEnrollmentThreadHandle;
        private Variables objVariables;
        private VistaRegistro objPrincipal;

        public void StartEnrollment(Reader objCurrentReader, VistaRegistro objPrincipal)
        {
            InitializeObjects();
            this.objPrincipal = objPrincipal;
            objVariables.currentReader = objCurrentReader;
            StartThreadEnrollment();
        }

        private void InitializeObjects()
        {
            objVariables = new Variables();
            objDevice = new SelectorHuella.Metodos();
        }

        public void StartThreadEnrollment()
        {
            objEnrollmentThreadHandle = new Thread(EnrollThread);
            objEnrollmentThreadHandle.IsBackground = true;
            objEnrollmentThreadHandle.Start();
        }

        private void EnrollThread()
        {

            objVariables.count = 0;

            objPrincipal.txtSalida.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                objPrincipal.txtSalida.AppendText("Se inicio el registro de huellas. \nColocar el dedo en el lector.");
            }));

            while (!objVariables.reset)
            {
                DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, CaptureAndExtractFmd());

                if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("El registro de huella fue exitoso.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                    objVariables.serializacion = Fmd.SerializeXml(resultEnrollment.Data);
                    objVariables.count = 0;
                    objVariables.reset = true;

                    objPrincipal.EndEnrollment(objVariables.serializacion, true);
                }
            }

            if (objVariables.currentReader != null)
                objVariables.currentReader.Dispose();
        }

        private IEnumerable<Fmd> CaptureAndExtractFmd()
        {
            while (!objVariables.reset)
            {
                DataResult<Fmd> resultConversion;

                try
                {
                    if (objVariables.count >= 8)
                    {
                        MessageBox.Show("El registro no se completó, vuelve a intentarlo.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);

                        objVariables.count = 0;
                        break;
                    }

                    Fid fid = null;

                    if (objVariables.currentReader == null)
                    {
                        objVariables.currentReader = objDevice.IndexDevice();
                        objDevice.InitializeDevice(ref objVariables.currentReader);
                    }

                    if (!CaptureFinger(ref fid))
                    {
                        //break;
                    }

                    if (fid == null)
                    {
                        continue;
                    }

                    objVariables.count++;

                    resultConversion = FeatureExtraction.CreateFmdFromFid(fid, Constants.Formats.Fmd.ANSI);

                    objPrincipal.txtSalida.Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        objPrincipal.txtSalida.AppendText("\nLa huella se ha capturado. Captura Numero: " + objVariables.count);
                    }));

                    if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        break;
                    }

                }
                catch (Exception)
                {
                    break;
                }

                yield return resultConversion.Data;
            }
        }

        public bool CaptureFinger(ref Fid fid)
        {
            try
            {
                Constants.ResultCode result = objVariables.currentReader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {

                    if (objVariables.currentReader != null)
                    {
                        objVariables.currentReader.Dispose();
                        objVariables.currentReader = null;
                    }

                    return false;
                }

                if ((objVariables.currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);

                    return true;
                }
                else 
                if ((objVariables.currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    objVariables.currentReader.Calibrate();
                }
                else 
                if ((objVariables.currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    if (objVariables.currentReader != null)
                    {
                        objVariables.currentReader.Dispose();
                        objVariables.currentReader = null;
                    }

                    return false;
                }

                CaptureResult captureResult = objVariables.currentReader.Capture(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 5000, objVariables.currentReader.Capabilities.Resolutions[0]);

                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (objVariables.currentReader != null)
                    {
                        objVariables.currentReader.Dispose();
                        objVariables.currentReader = null;
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
                    return true;
                }

                fid = captureResult.Data;

                return true;
            }
            catch
            {
                if (objVariables.currentReader != null)
                {
                    objVariables.currentReader.Dispose();
                    objVariables.currentReader = null;
                }
                return false;
            }
        }

        public void StopEnrollment()
        {
            if (objVariables.currentReader != null)
            {
                objVariables.reset = true;
                objVariables.currentReader.CancelCapture();

                if (objEnrollmentThreadHandle != null)
                {
                    objEnrollmentThreadHandle.Join(5000);
                }
            }
            else
            {
                objVariables.reset = true;
                if (objEnrollmentThreadHandle != null)
                {
                    objEnrollmentThreadHandle.Join(5000);
                }
            }
        }
    }
}
