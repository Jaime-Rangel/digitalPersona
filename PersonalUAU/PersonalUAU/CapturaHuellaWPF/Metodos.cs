using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Globalization;

namespace PersonalUAU.CapturaHuellaWPF
{
    public class Metodos
    {
        //Atributos
        SelectorHuella.Metodos device;
        Thread threadHandle;
        Variables variables;

        public Metodos(Reader objReader, System.Windows.Controls.Image pbFingerprint)
        {
            variables = new Variables();
            variables.patchCapturaHuella = "c:\\Huellas\\";
            device = new SelectorHuella.Metodos();
            variables.currentReader = objReader;
            variables.pbFingerprint = pbFingerprint;
            device.InitializeDevice(ref objReader);

        }

        public void StartCaptures()
        {

            variables.pbFingerprint.Source = new BitmapImage(new Uri("pack://application:,,,/PersonalUAU;component/Resources/Preview_fingerprint.jpg", UriKind.Absolute));

            threadHandle = new Thread(CaptureThread);
            threadHandle.IsBackground = true;
            threadHandle.Start();
        }

        public void StopCaptures()
        {
            if (variables.currentReader != null)
            {
                variables.reset = true;
                variables.currentReader.CancelCapture();

                if (threadHandle != null)
                {
                    threadHandle.Join(5000);
                }
            }
            else
            {
                variables.reset = true;
                if (threadHandle != null)
                {
                    threadHandle.Join(5000);
                }
            }

        }

        private void CaptureThread()
        {
            variables.reset = false;

            while (!variables.reset)
            {
                Fid fid = null;

                if (variables.currentReader == null)
                {
                    variables.currentReader = device.IndexDevice();
                    device.InitializeDevice(ref variables.currentReader);
                }

                if (!CaptureFinger(ref fid))
                {
                    //break;
                }

                if (fid == null)
                {
                    continue;
                }

                foreach (Fid.Fiv fiv in fid.Views)
                {
                    SendMessage(CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }
            }

            if (variables.currentReader != null)
                variables.currentReader.Dispose();
        }

        private delegate void SendMessageCallback(object payload);

        private void SendMessage(object payload)
        {
            variables.pbFingerprint.Dispatcher.BeginInvoke(new Action(delegate()
            {
                Bitmap aux = (Bitmap)payload;
                variables.saved_picture = aux;
                variables.pbFingerprint.Source = BitmapToImageSource(aux);
            }));
        }

        public bool CaptureFinger(ref Fid fid)
        {
            try
            {
                Constants.ResultCode result = variables.currentReader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    MessageBox.Show("Get Status Error:  1/" + result);
                    if (variables.currentReader != null)
                    {
                        variables.currentReader.Dispose();
                        variables.currentReader = null;
                    }
                    return false;
                }

                if ((variables.currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                    return true;
                }
                else if ((variables.currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    variables.currentReader.Calibrate();
                }
                else if ((variables.currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    MessageBox.Show("Get Status:  2/" + variables.currentReader.Status.Status);
                    if (variables.currentReader != null)
                    {
                        variables.currentReader.Dispose();
                        variables.currentReader = null;
                    }

                    return false;
                }

                CaptureResult captureResult = variables.currentReader.Capture(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 5000, variables.currentReader.Capabilities.Resolutions[0]);

                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    Console.WriteLine("Error: " + captureResult.ResultCode);

                    if (variables.currentReader != null)
                    {
                        variables.currentReader.Dispose();
                        variables.currentReader = null;
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
                    MessageBox.Show("Quality Error:  4/" + captureResult.Quality);
                    return true;
                }

                fid = captureResult.Data;

                return true;
            }
            catch
            {
                Console.WriteLine("Ha ocurrido un error.");

                if (variables.currentReader != null)
                {
                    variables.currentReader.Dispose();
                    variables.currentReader = null;
                }

                return false;
            }
        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt32() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        public string SaveFingerprintImage(string Enum, string referencia)
        {
            string[] parametros;
            string folderPostFix = "";
            string folderToSave = "";
            string folderFile = "";
            string temp;

            referencia = referencia.ToUpper();
            parametros = referencia.Split('-');

            if (parametros.Count() == 1)
            {
                temp = parametros[0];
            }
            else
            {
                temp = parametros[1];
            }

            folderPostFix = GenerateDirectory(temp);
            folderToSave = "images" + Enum + "-" + folderPostFix;
            folderFile = "\\" + referencia;
            CreateDirectory(variables.patchCapturaHuella + folderToSave);
            StockImage(folderToSave, folderFile);

            return folderToSave + folderFile + ".jpg";
        }

        public string GenerateDirectory(string referencia)
        {
            string folderPostFix;

            if (Convert.ToInt32(referencia) >= 100)
            {
                folderPostFix = (Int32.Parse(referencia) / 100).ToString(CultureInfo.InvariantCulture);
            }
            else if (Convert.ToInt32(referencia) >= 10 && Convert.ToInt32(referencia) < 100)
            {
                folderPostFix = referencia.Insert(0, "0");
            }
            else
            {
                folderPostFix = referencia.Insert(0, "00");
            }

            return folderPostFix;
        }

        public void CreateDirectory(string ruta)
        {
            try
            {
                if (Directory.Exists(ruta) == false)
                {
                    DirectoryInfo di = Directory.CreateDirectory(ruta);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Convert.ToString(e));
            }
        }

        public void StockImage(string folderToSave, string folderFile)
        {
            try
            {
                variables.saved_picture.Save(variables.patchCapturaHuella + folderToSave + folderFile + ".jpg", ImageFormat.Jpeg);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }

        public string ReturnFingerprintImageRoute()
        {
            return variables.patchCapturaHuella;
        }

        public void DeleteImageDirectory(string folderFile)
        {
            try
            {
                File.Delete(folderFile);

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Hubo un error inesperado.",
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                Console.WriteLine(Ex);
            }
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}